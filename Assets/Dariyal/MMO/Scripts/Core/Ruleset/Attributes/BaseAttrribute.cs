using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dariyal.MMO.Core.Ruleset
{
    public class BaseAttrribute
    {
        private float _baseValue;
        private float _baseMultiplier;

        public float BaseValue { get { return _baseValue; } }
        public float BaseMultiplier { get { return _baseMultiplier; } }

        public BaseAttrribute(float value, float multiplier = 0)
        {
            _baseValue = value;
            _baseMultiplier = multiplier;
        }
    }

    public class RawBonus : BaseAttrribute
    {
        public RawBonus(float value = 0, float multiplier = 0)
            : base(value, multiplier)
        {
        }
    }

    public class FinalBonus : BaseAttrribute
    {
        public FinalBonus(float value = 0, float multiplier = 0)
            : base(value, multiplier)
        {
        }
    }

    public class TimedBonus : FinalBonus
    {
        private float _duration;
        Attribute _parent;

        public TimedBonus(float duration, float value = 0, float multiplier = 0)
            : base(value, multiplier)
        {
            _duration = duration;
        }

        public void StartBonus(Attribute parent)
        {
            _parent = parent;
            Timer();
        }

        public IEnumerable Timer()
        {
            yield return new WaitForSeconds(_duration);
            _parent.RemoveFinalBonus(this);
        }
    }

    public class Attribute : BaseAttrribute
    {
        private List<RawBonus> _rawBonuses;
        private List<FinalBonus> _finalBonuses;
        private float _finalValue;

        public float Value
        {
            get
            {
                return CalculateValue();
            }
        }

        public Attribute(float startingValue)
            : base(startingValue)
        {
            _rawBonuses = new List<RawBonus>();
            _finalBonuses = new List<FinalBonus>();
        }

        public void AddRawBonus(RawBonus bonus)
        {
            _rawBonuses.Add(bonus);
        }

        public void RemoveRawBonus(RawBonus bonus)
        {
            _rawBonuses.Remove(bonus);
        }

        public void AddFinalBonus(FinalBonus bonus)
        {
            _finalBonuses.Add(bonus);
        }

        public void RemoveFinalBonus(FinalBonus bonus)
        {
            _finalBonuses.Remove(bonus);
        }

        public void ApplyRawBonuses()
        {
            //Calculate raw bonus
            float rawBonusValue = 0;
            float rawBonusMultiplier = 0;

            foreach (RawBonus bonus in _rawBonuses)
            {
                rawBonusValue += bonus.BaseValue;
                rawBonusMultiplier += bonus.BaseMultiplier;
            }

            _finalValue += rawBonusValue;
            _finalValue *= (1 + rawBonusMultiplier);
        }

        public void ApplyFinalBonuses()
        {
            //calculate final bonus
            float finalBonusValue = 0;
            float finalBonusMultiplier = 0;

            foreach (FinalBonus bonus in _finalBonuses)
            {
                finalBonusValue += bonus.BaseValue;
                finalBonusMultiplier += bonus.BaseMultiplier;
            }

            _finalValue += finalBonusValue;
            _finalValue *= (1 + finalBonusMultiplier);
        }

        public virtual float CalculateValue()
        {
            _finalValue = BaseValue;

            ApplyRawBonuses();
            ApplyFinalBonuses();

            return _finalValue;
        }
    }

    public class DependentAttribute : Attribute
    {
        protected List<Attribute> _otherAttributes;

        public DependentAttribute(float startingValue)
            : base(startingValue)
        {
            _otherAttributes = new List<Attribute>();
        }

        public void AddAtrribute(Attribute attribute)
        {
            _otherAttributes.Add(attribute);
        }

        public void RemoveAtrribute(Attribute attribute)
        {
            _otherAttributes.Remove(attribute);
        }

        public override float CalculateValue()
        {
            return base.CalculateValue();
        }
    }
}
