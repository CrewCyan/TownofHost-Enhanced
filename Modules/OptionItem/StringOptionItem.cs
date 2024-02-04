namespace TOHE;

public class StringOptionItem(int id, string name, int defaultValue, TabGroup tab, bool isSingleValue, string[] selections, bool vanilla) : OptionItem(id, name, defaultValue, tab, isSingleValue, vanillaStr:vanilla)
{
    // 必須情報
    public IntegerValueRule Rule = (0, selections.Length - 1, 1);
    public string[] Selections = selections;

    public static StringOptionItem Create(
        int id, string name, string[] selections, int defaultIndex, TabGroup tab, bool isSingleValue, bool vanillaText = false
    )
    {
        return new StringOptionItem(
            id, name, defaultIndex, tab, isSingleValue, selections, vanillaText
        );
    }

    // Getter
    public override int GetInt() => Rule.GetValueByIndex(CurrentValue);
    public override float GetFloat() => Rule.GetValueByIndex(CurrentValue);
    public override string GetString()
    {
        return Translator.GetString(Selections[Rule.GetValueByIndex(CurrentValue)]);
    }
    public int GetChance()
    {
        //0%or100%の場合
        if (Selections.Length == 2) return CurrentValue * 100;

        //TOHE的职业生成模式
        if (Selections.Length == 3) return CurrentValue;

        //0%～100%or5%～100%の場合
        var offset = 12 - Selections.Length;
        var index = CurrentValue + offset;
        var rate = index <= 1 ? index * 5 : (index - 1) * 10;
        return rate;
    }
    public override int GetValue()
        => Rule.RepeatIndex(base.GetValue());

    // Setter
    public override void SetValue(int value, bool doSync = true)
    {
        base.SetValue(Rule.RepeatIndex(value), doSync);
    }
}