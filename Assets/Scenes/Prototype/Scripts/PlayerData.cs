
public class PlayerData 
{

    private int _normalSKill;
    private int _ultimateSkill;
    private int _Hp;
    public PlayerData(int defalutNormalSkill, int defaultUltimateSkill){

        this._Hp = 3;
        this._normalSKill = defalutNormalSkill;
        this._ultimateSkill = defaultUltimateSkill;
    }

    public ref int HpChange(){
        return ref _Hp;
    }

}
