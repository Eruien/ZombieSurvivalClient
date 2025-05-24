
// 각종 enum 값 구조체 저장
namespace Assets.Scripts
{
    public enum SoundType
    {
        None,
        Master,
        BGM,
        Effect,
    }

    public enum Team
    {
        None = 0,
        Citizen = 7,
        Enemy = 8,
    };

    public enum AttackType
    {
        None = 0,
        Melee = 1,
        Range = 2,
    }

    struct ObjectStat
    {
        public float maxhp;
        public float defense;
        public float attackSpeed;
        public AttackType attackType;
        public float attackRange;
        public float attackRangeCorrectionValue;
        public float defaultAttackDamage;
        public float moveSpeed;
        public float projectTileSpeed;
    }

    public enum CharacterType
    {
        None,
        Citizen,
        Zombie,
    }

    public enum State
    {
        None,
        Idle,
        Move,
        Attack,
    }

    public enum Trigger
    {
        None,
        InAttackDistance,
        OutAttackDistance,
    }
}

