using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public static StatusManager Instance;

    // ========== Inspector 可编辑的私有字段 ==========
    [Header("战斗属性")]
    public int _damage = 1;
    public float _weaponRange = 1.5f;
    public float _hitback = 20f;
    public float _hitbackTime = 0.25f;
    public float _attackCool = 0.5f;

    [Header("移动属性")]
    public int _speed = 5;

    [Header("生命属性")]
    public int _maxHealth = 5;
    public int _currentHealth = 5;

    // ========== 字典存储（存储引用，实现同步） ==========
    private Dictionary<string, object> stats = new Dictionary<string, object>();

    // ========== 公开属性（简洁访问，自动同步字典） ==========
    public int damage
    {
        get => _damage;
        set
        {
            _damage = value;
            stats["damage"] = value;
            OnStatChanged?.Invoke("damage", value);
        }
    }

    public float weaponRange
    {
        get => _weaponRange;
        set
        {
            _weaponRange = value;
            stats["weapon_range"] = value;
            OnStatChanged?.Invoke("weapon_range", value);
        }
    }

    public float hitback
    {
        get => _hitback;
        set
        {
            _hitback = value;
            stats["hitback"] = value;
            OnStatChanged?.Invoke("hitback", value);
        }
    }

    public float hitbackTime
    {
        get => _hitbackTime;
        set
        {
            _hitbackTime = value;
            stats["hitback_time"] = value;
            OnStatChanged?.Invoke("hitback_time", value);
        }
    }

    public float attackCool
    {
        get => _attackCool;
        set
        {
            _attackCool = value;
            stats["attack_cool"] = value;
            OnStatChanged?.Invoke("attack_cool", value);
        }
    }

    public int speed
    {
        get => _speed;
        set
        {
            _speed = value;
            stats["speed"] = value;
            OnStatChanged?.Invoke("speed", value);
        }
    }

    public int maxHealth
    {
        get => _maxHealth;
        set
        {
            _maxHealth = value;
            stats["max_health"] = value;
            OnStatChanged?.Invoke("max_health", value);

            // 如果当前血量超过新上限，同步调整
            if (currentHealth > value)
            {
                currentHealth = value;
            }
        }
    }

    public int currentHealth
    {
        get => _currentHealth;
        set
        {
            int newValue = Mathf.Clamp(value, 0, _maxHealth);
            _currentHealth = newValue;
            stats["cur_health"] = newValue;
            OnStatChanged?.Invoke("cur_health", newValue);
        }
    }
    
    // ========== 事件触发器，广播电台 ==========
    public event System.Action<string, object> OnStatChanged;       

    // ========== 单例初始化 ==========
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ========== 初始化字典（存储当前值的引用） ==========
    private void InitializeDictionary()
    {
        stats["damage"] = _damage;
        stats["weapon_range"] = _weaponRange;
        stats["hitback"] = _hitback;
        stats["hitback_time"] = _hitbackTime;
        stats["attack_cool"] = _attackCool;
        stats["speed"] = _speed;
        stats["max_health"] = _maxHealth;
        stats["cur_health"] = _currentHealth;
    }

    // ========== 通用字典访问方法（可选） ==========
    public T GetStat<T>(string key)
    {
        if (stats.TryGetValue(key, out object value))
        {
            return (T)value;
        }
        Debug.LogWarning($"未找到属性: {key}");
        return default;
    }

    public void SetStat<T>(string key, T value)
    {
        if (stats.ContainsKey(key))
        {
            stats[key] = value;
            OnStatChanged?.Invoke(key, value);

            // 同步到对应的私有字段
            switch (key)
            {
                case "damage": _damage = (int)(object)value; break;
                case "weapon_range": _weaponRange = (float)(object)value; break;
                case "hitback": _hitback = (float)(object)value; break;
                case "hitback_time": _hitbackTime = (float)(object)value; break;
                case "attack_cool": _attackCool = (float)(object)value; break;
                case "speed": _speed = (int)(object)value; break;
                case "max_health": _maxHealth = (int)(object)value; break;
                case "cur_health": _currentHealth = (int)(object)value; break;
            }
        }
    }
}