using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; // Add the TextMesh Pro namespace to access the various functions.
using System.Linq;
using UnityEngine.InputSystem;
using System;

public class HandAnim : MonoBehaviour
{
    public Animator m_animator = null;
    [Header("Input")]
    [SerializeField] public InputActionProperty GripAction;
    [SerializeField] public InputActionProperty TriggerAction;
    [SerializeField] public InputActionProperty IndexAction;
    [SerializeField] public InputActionProperty ThumbAction;


    public const string ANIM_LAYER_NAME_POINT = "Point Layer";
    public const string ANIM_LAYER_NAME_THUMB = "Thumb Layer";
    public const string ANIM_PARAM_NAME_FLEX = "Flex";
    public const string ANIM_PARAM_NAME_POSE = "Pose";

    private int m_animLayerIndexThumb = -1;
    private int m_animLayerIndexPoint = -1;
    private int m_animParamIndexFlex = -1;
    private int m_animParamIndexPose = -1;
    private Collider[] m_colliders = null;

    public float anim_frames = 4f;
    private float grip_state = 0f;
    private float trigger_state = 0f;
    private float triggerCap_state = 0f;
    private float thumbCap_state = 0f;

    private void Reset()
    {
        m_animator = GetComponentInChildren<Animator>();
    }

    private void Awake()
    {
        GripAction.action.performed += GripActionPerformed;
        TriggerAction.action.performed += TriggerActionPerformed;
        IndexAction.action.performed += IndexActionPerformed;
        ThumbAction.action.performed += ThumbActionPerformed;
        GripAction.action.Enable();
        TriggerAction.action.Enable();
        IndexAction.action.Enable();
        ThumbAction.action.Enable();
    }

    void Start()
    {
        m_colliders = this.GetComponentsInChildren<Collider>().Where(childCollider => !childCollider.isTrigger).ToArray();
        for (int i = 0; i < m_colliders.Length; ++i)
        {
            Collider collider = m_colliders[i];
            // collider.transform.localScale = new Vector3(COLLIDER_SCALE_MIN, COLLIDER_SCALE_MIN, COLLIDER_SCALE_MIN);
            collider.enabled = true;
        }
        m_animLayerIndexPoint = m_animator.GetLayerIndex(ANIM_LAYER_NAME_POINT);
        m_animLayerIndexThumb = m_animator.GetLayerIndex(ANIM_LAYER_NAME_THUMB);
        m_animParamIndexFlex = Animator.StringToHash(ANIM_PARAM_NAME_FLEX);
        m_animParamIndexPose = Animator.StringToHash(ANIM_PARAM_NAME_POSE);
    }

    private void ThumbActionPerformed(InputAction.CallbackContext obj)
    {
        CalculateState(obj.ReadValue<float>(), ref thumbCap_state);
        m_animator.SetLayerWeight(m_animLayerIndexThumb, 1f - thumbCap_state);
    }

    private void IndexActionPerformed(InputAction.CallbackContext obj)
    {
        CalculateState(obj.ReadValue<float>(), ref triggerCap_state);
        m_animator.SetLayerWeight(m_animLayerIndexPoint, 1f - triggerCap_state);
    }

    private void TriggerActionPerformed(InputAction.CallbackContext obj)
    {
        CalculateState(obj.ReadValue<float>(), ref trigger_state);
        m_animator.SetFloat("Pinch", trigger_state);
    }

    private void GripActionPerformed(InputAction.CallbackContext obj)
    {
        CalculateState(obj.ReadValue<float>(), ref grip_state);
        m_animator.SetFloat(m_animParamIndexFlex, grip_state);
    }

    private void CalculateState(float value, ref float state)
    {
        var delta = value - state;
        if (delta > 0f)
        {
            state = Mathf.Clamp(state + 1 / anim_frames, 0f, value);
        }
        else if (delta < 0f)
        {
            state = Mathf.Clamp(state - 1 / anim_frames, value, 1f);
        }
        else
        {
            state = value;
        }
    }
}