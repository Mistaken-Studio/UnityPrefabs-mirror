using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Mistaken.UnityPrefabs.Text
{
    [PublicAPI]
    public class TextGenerator : MonoBehaviour
    {
        [Multiline]
        public string DebugText = "";

        public float VerticalDistance = 2f;
        public float HorizontalDistance = 1f;

        public Color TextColor = Color.black;

        [SerializeField]
        public List<Character> Characters;

        public event Action<TextGenerator> OnTextChanged
        {
            add => onTextChanged += value;
            remove => onTextChanged -= value;
        }

        private event Action<TextGenerator> onTextChanged;

        public void SetText(string text)
        {
            ClearText();

            float xOffset = 0;
            float yOffset = 0;
            var alfabet = Characters.ToDictionary(x => x.character, x => x.prefab);

            foreach (var line in text.ToUpper().Split('\n'))
            {
                foreach (var letter in line/*.Reverse()*/)
                {
                    switch (letter)
                    {
                        case ' ':
                            xOffset += 5 + HorizontalDistance;
                            continue;
                    }

                    if (!alfabet.TryGetValue(letter, out var prefab))
                        continue;

                    var spawned = GameObject.Instantiate(prefab, this.transform);
                    spawned.transform.localPosition = new Vector3(xOffset, yOffset);

                    foreach (var item in spawned.GetComponentsInChildren<MeshRenderer>())
                    {
                        item.transform.localScale = item.transform.localScale * -1f;
                        item.transform.localEulerAngles += Vector3.up * 180f;
                    }
                    xOffset += 5 + HorizontalDistance;
                }

                xOffset = 0;
                yOffset -= 7 + VerticalDistance;
            }

            onTextChanged?.Invoke(this);
        }

        public void ClearText()
        {
            foreach (var item in this.GetComponentsInChildren<Transform>().Where(x => x.parent == this.transform).ToArray())
                GameObject.DestroyImmediate(item.gameObject, true);
        }

        [Serializable]
        public struct Character
        {
            public char character;
            public GameObject prefab;
        }

        private void Awake()
        {
            foreach (var item in this.GetComponentsInChildren<MeshRenderer>())
                item.material.color = TextColor;
        }
    }
}