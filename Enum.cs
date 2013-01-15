using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct RECT
{
    public int left, top, right, bottom;
}

[StructLayout(LayoutKind.Sequential)]
struct CModeMgr
{
    public int m_loopCond;
    public int m_curMode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x28)]
    public char[] m_curModeName;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x28)]
    public char[] m_nextModeName;
    public int m_curModeType;
    public int m_nextModeType;
}

[StructLayout(LayoutKind.Sequential)]
struct Session
{
    public int m_curMapType;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0xF * 0x1E)]
    public int[] m_mapInfoTable;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x54E)]
    public byte[] m_unk_;
    public CList m_guildSkillList;
    public CList m_skillInfoList;
    public CList m_homunSkillList;
    public CList m_merSkillList;
    public CList m_QuestList;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x6C0)]
    public byte[] m_unk;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x28)]
    public char[] m_selectedServerName;
    public int m_diffTime;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x14)]
    public char[] m_curMap;
    public int m_numLatePacket;
    public int m_showType;
    public UInt32 m_averagePingTime;
    public UInt32 m_showDigitTick;
    public UInt32 m_killTimeStartTick;
    public int m_isShowTime;
    public int m_isNeverDie;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x40)]
    public char[] m_xName;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x40)]
    public char[] m_aName;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x12)]
    public TAG_CHARACTER_BLOCK_INFO[] m_charBlockInfo2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x91)]
    public int[] m_unk1;
    public int m_sex;
    public int m_charNum;
    public int m_authCode;
    public UInt32 m_userLevel;
    public UInt32 m_lastLoginIP;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x1C)]
    public byte[] m_lastLoginTime;
    public int m_mkcount;
    public int m_temp2;
    public int m_haircolor;
    public int m_deadcount;
    public int m_head;
    public int m_weapon;
    public int m_shield;
    public int m_bodyPalette;
    public int m_headPalette;
    public int m_accessory;
    public int m_accessory2;
    public int m_accessory3;
    public int m_bodyState;
    public int m_healthState;
    public int m_effectState;
    public int m_posX;
    public int m_posY;
    public int m_dir;
    public char m_camp;
    public int m_camp_A;
    public int m_camp_B;
    public char m_charfont;
    public short m_DayCount;
    public int m_cartCurCount;
    public int m_cartMaxCount;
    public int m_cartCurWeight;
    public int m_cartMaxWeight;
    public int m_maxNumOfSellItemOfMerchant;
    public int m_attackRange;
    public int m_charSlot;
    public int m_BgmVolume;
    public float m_LoadingTime;
    public int m_isShowWhisperWnd;
    public int m_isPlayWhisperOpenSound;
    public int m_isShowWhisperWnd_Friend;
    public int m_isItemSnap;
    public int m_isShowGameOver;
    public int m_monsterSnapOn_Skill;
    public int m_monsterSnapOn_NoSkill;
    public int m_isShowTeamGravityPlanetLogo;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x7 * 0x5 * 0x5)]
    public int[] m_petEmotionTable;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x440)]
    public byte[] m_unk2;
    public int m_guildDealZeny;
    public int m_MaxItemIndex;
    public int m_aid;
    public int m_gid;
    public int m_isWeaponBow;
    public float m_oneCellDist;
    public int m_job;
    public int m_exp;
    public int m_level;
    public int m_point;
    public int m_nextexp;
    public int m_joblevel;
    public int m_skillPoint;
    public int m_guildSkillPoint;
    public int m_homunSkillPoint;
    public int m_merSkillPoint;
    public int m_plusStr;
    public int m_plusAgi;
    public int m_plusVit;
    public int m_plusInt;
    public int m_plusDex;
    public int m_plusLuk;
    public int m_str;
    public int m_agi;
    public int m_vit;
    public int m_int;
    public int m_dex;
    public int m_luk;
    public int m_standardStr;
    public int m_standardAgi;
    public int m_standardVit;
    public int m_standardInt;
    public int m_standardDex;
    public int m_standardLuk;
    public int m_ASPD;
    public int m_attPower;
    public int m_mdefPower;
    public int m_plusASPD;
    public int m_itemDefPower;
    public int m_plusdefPower;
    public int m_refiningPower;
    public int m_max_mattPower;
    public int m_min_mattPower;
    public int m_plusmdefPower;
    public int m_hitSuccessValue;
    public int m_avoidSuccessValue;
    public int m_criticalSuccessValue;
    public int m_plusAvoidSuccessValue;
    public int m_equipArrowIndex;
    public int m_gold;
    public int m_speed;
    public int m_honor;
    public int m_maxWeight;
    public int m_jobnextexp;
    public int m_jobexp;
    public int m_weight;
    public int m_virtue;
    public int m_isMonsterSnap;
    public int m_systemDiffTime;
    public CList m_itemlist;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x20A8)]
    public byte[] m_unk3;
    public int m_initMapPos;
    public int m_maxNum;
    public IntPtr m_spIndex;
    public IntPtr m_hpIndex;
    public IntPtr m_xorIndex;
    public IntPtr m_maxspIndex;
    public IntPtr m_maxhpIndex;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0xC)]
    public int[] m_xorValue;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0xDC)]
    public byte[] m_unk4;
    public int m_fogOn;
    public int m_unk5;
    public int m_unk6;
    public int m_isNoCtrl;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0xBC)]
    public byte[] m_unk7;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x40)]
    public char[] m_pName;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x5F4)]
    public byte[] m_unk8;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x40)]
    public char[] m_cName;
};

[StructLayout(LayoutKind.Sequential)]
struct CList
{
    public IntPtr AllocatorStuff;
    public IntPtr _MyHead;
    public int _MySize;
    public IntPtr _End;
}

[StructLayout(LayoutKind.Sequential)]
struct TAG_CHARACTER_BLOCK_INFO
{
    public UInt32 GID;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x14)]
    public char[] szExpireDate;
}