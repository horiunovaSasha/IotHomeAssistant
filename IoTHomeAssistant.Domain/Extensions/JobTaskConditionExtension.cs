using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;
using System;
using System.Linq;

namespace IoTHomeAssistant.Domain.Extensions
{
    public static class JobTaskConditionExtension
    {
        public static bool CompareFloat(this JobTaskCondition condition, string value)
        {
            if (float.TryParse(value, out var fValue) &&
                float.TryParse(condition.Value, out var fItemValue) &&
                condition.Operation.HasValue)
            {
                return Compare(condition.Operation.Value, fItemValue, fValue);
            }

            return false;
        }

        public static bool Compare(this JobTaskCondition condition, DateTime value)
        {
            if (!condition.Operation.HasValue)
                return false;

            var conditionValue = condition.DateTime;

            if (conditionValue.Year == 1 && conditionValue.Month == 1 && conditionValue.Day == 1)
            {
                conditionValue = conditionValue.AddYears(value.Year - 1);
                conditionValue = conditionValue.AddMonths(value.Month - 1);
                conditionValue = conditionValue.AddDays(value.Day - 1);
            }

            switch (condition.Operation.Value)
            {
                case Enums.ConditionOperationEnum.Equal:
                    return conditionValue == value;
                case Enums.ConditionOperationEnum.NotEqual:
                    return conditionValue != value;
                case Enums.ConditionOperationEnum.More:
                    return value > conditionValue;
                case Enums.ConditionOperationEnum.Less:
                    return value < conditionValue;
                case Enums.ConditionOperationEnum.MoreOrEqual:
                    return value >= conditionValue;
                case Enums.ConditionOperationEnum.LessOrEqual:
                    return value <= conditionValue;
                default:
                    return false;
            }
        }

        public static bool Compare(this JobTaskCondition condition, string value)
        {
            if (!condition.Operation.HasValue)
                return false;

            switch (condition.Operation.Value)
            {
                case Enums.ConditionOperationEnum.Equal:
                    return condition.Value == value;
                case Enums.ConditionOperationEnum.NotEqual:
                    return condition.Value != value;
                default:
                    return false;
            }
        }

        public static bool Compare(this JobTaskCondition condition, DeviceEventDto deviceEvent, string value)
        {
            if (condition.Operation.HasValue && deviceEvent.ValueType != null && deviceEvent.ValueType.Items.Any())
            {
                var conditionValues = deviceEvent.ValueType.Items.Select(x => x.Value);

                switch (condition.Operation.Value)
                {
                    case Enums.ConditionOperationEnum.Equal:
                        return conditionValues.Contains(value);
                    case Enums.ConditionOperationEnum.NotEqual:
                        return !conditionValues.Contains(value);
                    default:
                        return false;
                }
            }

            return false;
        }

        private static bool Compare(Enums.ConditionOperationEnum operation, float conditionValue, float value)
        {
            switch (operation)
            {
                case Enums.ConditionOperationEnum.Equal:
                    return conditionValue == value;
                case Enums.ConditionOperationEnum.NotEqual:
                    return conditionValue != value;
                case Enums.ConditionOperationEnum.More:
                    return conditionValue > value;
                case Enums.ConditionOperationEnum.Less:
                    return conditionValue < value;
                case Enums.ConditionOperationEnum.MoreOrEqual:
                    return conditionValue >= value;
                case Enums.ConditionOperationEnum.LessOrEqual:
                    return conditionValue <= value;
                default:
                    return false;
            }
        }
    }
}
