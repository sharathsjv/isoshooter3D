using UnityEngine;
using System.Collections;
using RootMotion;

namespace RootMotion.Dynamics {

    [AddComponentMenu("Scripts/RootMotion.Dynamics/PuppetMaster/Behaviours/BehaviourBalance")]
    public class BehaviourBalanceRecover : BehaviourBase {

        protected override string GetTypeSpring() {
            return typeSpring;
        }

        private const string typeSpring = "BehaviourBalanceRecover";

        [Header("Animation")]
        public string stateName = "BalanceFall";
        public float transitionDuration = 0.25f;
        public int layer = 0;
        public float fixedTime = 0f;

        [Header("Exit")]
        public bool autoExit = true;
        public float duration = 0.75f;

        public PuppetEvent onFinished;

        private float timer;

        [Header("Fall Direction")]

        public string fallXParameter = "FallX";
        public string fallZParameter = "FallZ";

        [Range(0f, 1f)]
        public float velocityWeight = 0.4f;

        [Range(0f, 1f)]
        public float leanWeight = 0.6f;

        public float directionSmooth = 8f;
        public float minVelocity = 0.25f;

        protected override void OnActivate() {
            timer = 0f;

            puppetMaster.targetAnimator.CrossFadeInFixedTime(
                stateName,
                transitionDuration,
                layer,
                fixedTime
            );

            StopAllCoroutines();
            StartCoroutine(SmoothActivate());
        }

        IEnumerator SmoothActivate() {

            foreach (Muscle m in puppetMaster.muscles) {
                m.state.pinWeightMlp = 0f;
                m.state.mappingWeightMlp = 0f;
            }

            float t = 0f;

            while (t < 1f) {
                t += Time.deltaTime;

                foreach (Muscle m in puppetMaster.muscles) {
                    m.state.mappingWeightMlp += Time.deltaTime;
                }

                yield return null;
            }
        }

        protected override void OnFixedUpdate(float deltaTime)
{
    Rigidbody hips = puppetMaster.muscles[0].rigidbody;

    // -------------------------
    // Velocity Direction
    // -------------------------

    Vector3 velocityDir = Vector3.zero;

    Vector3 planarVelocity = Vector3.ProjectOnPlane(
        hips.linearVelocity,
        puppetMaster.targetRoot.up
    );

    if (planarVelocity.sqrMagnitude > minVelocity * minVelocity)
    {
        velocityDir = planarVelocity.normalized;
    }

    // -------------------------
    // Lean Direction
    // -------------------------

    Vector3 leanDir = Vector3.ProjectOnPlane(
        -hips.transform.up,
        puppetMaster.targetRoot.up
    );

    if (leanDir.sqrMagnitude > 0.0001f)
        leanDir.Normalize();

    // -------------------------
    // Combine
    // -------------------------

    Vector3 fallDir =
        velocityDir * velocityWeight +
        leanDir * leanWeight;

    if (fallDir.sqrMagnitude > 0.0001f)
        fallDir.Normalize();

    // Convert to character local space
    Vector3 local =
        puppetMaster.targetRoot.InverseTransformDirection(fallDir);

    // Smooth animator values
    float x = Mathf.MoveTowards(
        puppetMaster.targetAnimator.GetFloat(fallXParameter),
        local.x,
        directionSmooth * deltaTime);

    float z = Mathf.MoveTowards(
        puppetMaster.targetAnimator.GetFloat(fallZParameter),
        local.z,
        directionSmooth * deltaTime);

    puppetMaster.targetAnimator.SetFloat(fallXParameter, x);
    puppetMaster.targetAnimator.SetFloat(fallZParameter, z);

    // Existing timer logic
    timer += deltaTime;

    if (autoExit && timer >= duration)
    {
        onFinished.Trigger(puppetMaster);
    }
}

        protected override void OnDeactivate() { }

        public override void OnReactivate() {
            timer = 0f;
        }

        public override void OnMuscleReconnected(Muscle m) {
            m.state.pinWeightMlp = 0f;
            m.state.mappingWeightMlp = 1f;
        }
    }
}