using PLAYERTWO.PlatformerProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Up : Collectable
{
    public int statNum;
    public override void Collect(Player player)
    {
        if (!m_vanished && !m_ghosting)
        {
            if (!hidden)
            {
                Vanish();
                player.stats.Change(statNum);

                if (particle != null)
                {
                    particle.Play();
                }
            }
            else
            {
                StartCoroutine(QuickShowRoutine());
            }

            StartCoroutine(CollectRoutine(player));
        }        
    }
}
