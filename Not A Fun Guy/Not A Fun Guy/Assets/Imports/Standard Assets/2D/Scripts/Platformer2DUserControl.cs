using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]

    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool playing;
        private bool m_Jump;
        private AudioSource walk;


        private void Awake()
        {
            walk = gameObject.GetComponents<AudioSource>()[1];
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            if (h == 0)
            {
                if (playing)
                {
                    walk.Stop();
                    playing = false;
                }
                m_Character.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                if (!playing && PlatformerCharacter2D.m_Grounded)
                {
                    walk.Play();
                    playing = true;
                }
                else if (!PlatformerCharacter2D.m_Grounded)
                {
                    walk.Stop();
                    playing = false;
                }
                m_Character.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
               // character.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            

            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
