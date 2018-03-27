using cn.bmob.io;

/// <summary>
/// 排名类
/// </summary>
public class Rank : BmobTable
{
    public string playerName { get; set; }      //名字
    public BmobInt score { get; set; }          //分数

    //-----------------------------------------------------------------

    /// <summary>
    /// 重写读数据的方法
    /// </summary>
    public override void readFields(BmobInput input)
    {
        base.readFields(input);

        this.playerName = input.getString("playerName");
        this.score = input.getInt("score");
    }

    //-----------------------------------------------------------------

    /// <summary>
    /// 重写写数据的方法
    /// </summary>
    public override void write(BmobOutput output, bool all)
    {
        base.write(output, all);

        output.Put("playerName", this.playerName);
        output.Put("score", this.score);
    }

    //-----------------------------------------------------------------
}