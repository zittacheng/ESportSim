using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;
using TMPro;

namespace ESP
{
    public class EventRenderer_Thread : EventRenderer {
        public SpriteRenderer ThreadSprite;
        public Sprite DefaultActiveSprite;
        public TextMeshPro HeroText;

        public override void Render()
        {
            base.Render();
            if (GetTarget())
            {
                if (GetTarget().ThreadSpriteOverride)
                {
                    ThreadSprite.sprite = GetTarget().ThreadSpriteOverride;
                    NameText.gameObject.SetActive(false);
                    HeroText.gameObject.SetActive(true);
                    HeroText.text = GetTarget().GetDisplayName();
                }
                else
                {
                    ThreadSprite.sprite = DefaultActiveSprite;
                    NameText.gameObject.SetActive(true);
                    HeroText.gameObject.SetActive(false);
                }
            }
            else
            {
                HeroText.text = "";
            }
        }
    }
}