﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float initialValue;

    public float RuntimeValue;

    public float maxHearths;

    //public float maxValue;

    [HideInInspector]
    public void OnBeforeSerialize()
    {

    }

    public void OnAfterDeserialize()
    {
        this.RuntimeValue = this.initialValue;
        this.maxHearths = this.initialValue;
    }

    public void setHeartContainer(float sumHearths)
    {
        //++this.initialValue;
        this.maxHearths += sumHearths;

        //this.RuntimeValue = this.RuntimeValue + (this.maxValue - this.initialValue);
        this.RuntimeValue = this.maxHearths;
    }

    public void setPlayerHealth(float sumHearths)
    {
        this.maxHearths += sumHearths;
        //this.initialValue += 2;
        //this.RuntimeValue = this.RuntimeValue + (this.maxValue - this.initialValue); 

        this.RuntimeValue = this.maxHearths;
    }

    public void recoverPlayerHealth()
    {
        if (this.RuntimeValue < this.maxHearths) {
            ++this.RuntimeValue;
        }
    }
}
