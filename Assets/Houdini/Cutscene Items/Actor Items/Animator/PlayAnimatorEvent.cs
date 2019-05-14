using UnityEngine;

namespace CinemaDirector
{
    [CutsceneItem("Animator", "Play Mecanim Animation", CutsceneItemGenre.ActorItem, CutsceneItemGenre.MecanimItem)]
    public class PlayAnimatorEvent : CinemaActorEvent
    {
        public string StateName;
        public int Layer = -1;

        public override void Trigger(GameObject actor)
        {
            Animator animator = actor.GetComponent<Animator>();
            if (animator == null)
            {
                return;
            }

            animator.Play(StateName, 0, 0);
        }
    }
}