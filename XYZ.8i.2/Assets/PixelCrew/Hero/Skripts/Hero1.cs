using Assets.PixelCrew.Hero.Skripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Assets.Utils;
using UnityEditor;
using UnityEditor.Animations;



namespace Assets.PixelCrew.Hero.Skripts
{
    public class Hero1 : MonoBehaviour
    {
        [SerializeField] private CheckCircleOverlap _attackRange;
        [SerializeField] private int _damage;
        [SerializeField] private Vector3 _groundCheckPositionDelta;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private bool _isArmed;


        [Space] [Header("PoisonAntidote")]
        [SerializeField] private int _antidotescore = 0;
        [SerializeField] private int _antidote;
        [SerializeField] private int _poison = 0; //мешает стакаться

        [Space] [Header("Coins")]       
        [SerializeField] private int _coins = 0;

        [Space] [Header("MovementRepulsion")]  
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpspeed;
        [SerializeField] private float _damageJumpSpeed;
        [SerializeField] private float _damageJumpSpeedPoins;
        [SerializeField] private float _interactionRadius;
        [SerializeField] private float _DamageJumpSpeed;
        [SerializeField] private float _fallVelocity;

        [Space] [Header("Layers")]    
        [SerializeField] private LayerMask _groundlayer;
        [SerializeField] private LayerMask _BarrelLayer;
        [SerializeField] private LayerCheck _GroundCheck;
        [SerializeField] private LayerCheck _BarrelCheck;
        [SerializeField] private LayerMask _interactionLayer;

        [Space] [Header("UnityEvent")]
        [SerializeField] private UnityEvent _action;
        
        [Space] [Header("Particles")]
        [SerializeField] private SpawnComponent _footParticles;
        [SerializeField] private SpawnComponent _JumpParticles;
        [SerializeField] private SpawnComponent _JumpParticlesDouble;
        [SerializeField] private SpawnComponent _fallPosition;
        [SerializeField] private ParticleSystem _hitParticalse;
        [SerializeField] private ParticleSystem _hitParticalsePoins;


        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;



        private readonly Collider2D[] _interactionResult = new Collider2D[1];
        private Rigidbody2D _rigidbody2d;
        private float _directionX;
        private float _directionY;
        private Animator _animator;
        private SpriteRenderer _sprite;

        //это анимационные ключи, они экономят память, юзаются в FixedUpdate, вместо них можно вообще сразу юзать названия "is-grounded" и итд, но память будет утекать быстроъ
        private static readonly int IsGround = Animator.StringToHash("is-grounded");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int IsRunning = Animator.StringToHash("is-runing");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Attackk = Animator.StringToHash("Attack");
        private bool _allowDoubleJump;
        private bool _IsGrounded;

        


        private void Awake()
        {
        _rigidbody2d = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _sprite = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _IsGrounded = IsGrounded();
        }

        private void HealthUpdate()
        {

        }

        public void SetDirectionX(float direction)
        {
            _directionX = direction;
        }

        internal void SetDirectionY(float directionY)
        {
            _directionY = directionY;
        }








        private void FixedUpdate()
        {
            var xVelocity = _directionX * _speed;
            var yVelocity = CalculateYVelocity();

            _rigidbody2d.velocity = new Vector2(xVelocity, yVelocity);

            _animator.SetBool(IsRunning, _directionX != 0);
            _animator.SetFloat(VerticalVelocity, _rigidbody2d.velocity.y);
            _animator.SetBool(IsGround, _IsGrounded);

            UpdateSpriteDerectional();
        }

        private float CalculateYVelocity()
        {
            var yVelocity = _rigidbody2d.velocity.y;
            var IsJumpPressing = _directionY > 0;

            if (_IsGrounded) _allowDoubleJump = true;

            if (IsJumpPressing)
            {
                yVelocity = CalculateJumpVelocity(yVelocity);


            }
            else if (_rigidbody2d.velocity.y > 0)
            {
                yVelocity *= 0.5f;
            }
            return yVelocity;
        }

        private float CalculateJumpVelocity(float yVelocity)
        {
            var isFalling = _rigidbody2d.velocity.y <= 0.001f;
            if (!isFalling) return yVelocity;

            if (_IsGrounded)
            {
                yVelocity += _jumpspeed;
                _JumpParticles.Spawn();
            }
            else if (_allowDoubleJump)
            {
                yVelocity = _jumpspeed;
                _JumpParticlesDouble.Spawn();
                _allowDoubleJump = false;
            }
            return yVelocity;
        }







        private void UpdateSpriteDerectional()
        {
            if (_directionX > 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // Verctor3(1, 1, 1) это то же самое что и Verctor3.one
                                                              // раньше было так _sprite.flipX = false; он чтоб партикл тоже поворачивался - переписали
            }
            else if (_directionX < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // раньше было так _sprite.flipX = true; он чтоб партикл тоже поворачивался - переписали
            }
        }


        private bool IsBarrelTouch()
        {
            return _BarrelCheck.IsTouchingLayer;
        }

        private bool IsGrounded()
        {
            return _GroundCheck.IsTouchingLayer;
        }


        //В ДЗ 8, 1Ч 23 МИН ОБЪЯСНЯЕТ ЧТО Handles работает только в UNITY_EDITOR, и вот эта хрень нужны чтоб при
        //компиляции в другой сборке юнити для билдов на телефоны ее просто нет, и чтоб не ломалось оно само себя вырежет
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = IsGrounded() ? HandlesUtils.TransparentGreen : HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position + _groundCheckPositionDelta, Vector3.forward, _groundCheckRadius);
        }
#endif


        public void SaySomething()
        {
            //Debug.Log("Вы нажали ЛКМ");
        }



        //public IEnumerator DamagePoison(int i) //принимаем 1
        //{
        //    Debug.Log("принял2");
        //    if (i==1)
        //    {   
        //        Debug.Log("начинаю ждать");

        //        yield return new WaitForSeconds(2);  // ждем 2 сек

        //        if(_antidote == 0) //если за эти 2 сек не успели выпить антидот, то 
        //        {
        //            i=0; //пусть будет

        //            TakeDamage();
        //            Debug.Log("подождал");
        //        }  
        //    } 
        //}

        //public IEnumerator PoinsLow(int j) //принял 1
        //{
        //    Debug.Log("принял1");
            
        //    if (j == 1)
        //    {
        //        _antidote = 0;  //обнуляем, если этого не сделать, то антидот подобранный до яда сработает и яда не будет
        //        for (int p=0; p < 10; p++) //говорим выполнять отнятие 10 раз
        //        {
        //            if(_antidotescore==1)
        //            {
        //                break;
        //                _antidotescore = 0; // чтоб опять можно было остановить когда угодно
        //                _antidote = 0; // обнуляем что, новый яд работал
        //                _poison = 0; //возвращаем в 0 чтоб проверка в Poins сработала когдакоснемся яда вновь
        //            }
        //            if(_antidote==0) // каждый раз перед отниманием, проверяем, не выпили ли мы антидот и кол во хп
        //            {
        //                yield return StartCoroutine(DamagePoison(1)); //переходим к DamagePoison
        //            }
                    
        //        }
        //        _poison = 0; //возвращаем в 0 чтоб проверка в Poins сработала когдакоснемся яда вновь
        //        _antidote = 0; // обнуляем что новый яд работал
        //        _antidotescore = 0; // чтоб опять можно было остановить когда угодно
        //    }
        //}

        //public void Poins(int k)    //принял 1
        //{
        //    Debug.Log("принял0");
        //    if(k == 1)
        //    {
        //        _antidote = 0;
        //        _antidotescore = 0;
        //        _poison++;
            
        //        if (k == 1 && _poison==1) //если принял 1 и приняло его когда фор в PoinsLow не работает то
        //        {   
        //            StartCoroutine(PoinsLow(1));   //переходим к PoinsLow
        //        }
        //    }
        //}

        //public void Antidote (int l)
        //{
        //    _antidote = 1;
        //    _antidotescore = 1;

        //}

        public void TakeDamage()
        {
            _animator.SetTrigger(Hit);
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, _damageJumpSpeed);

            if (_coins > 0)
            {
                SapwnCoinsDamage();
            }
        }

        private void SapwnCoinsDamage()
        {
            var numCoinsToDispose = Mathf.Min(_coins, 5); // если у нас кол-во монет больше чем 5 то 5, если меньше, то все что осталось
            _coins -= numCoinsToDispose;
            Debug.Log(_coins);

            var burst = _hitParticalse.emission.GetBurst(0); // обращаемся к партикл системе, к чати где гогворим сколько выпускать, раздел - emissio, пункт - Burst
            burst.count = numCoinsToDispose;                // устанавливаем кол-во партиклов которы енужно выкинуть
            _hitParticalse.emission.SetBurst(0, burst);     //с сохраним это  в нашей притикл системе, раздел - emissio, пункт - Burst

            _hitParticalse.gameObject.SetActive(true);  // активируем пактикл систему ( по умолчанию она у нас выкл)
            _hitParticalse.Play(); // говорим ей что нужно проиграться
        }

        public void TakeDamagePoison()
        {
            _animator.SetTrigger(Hit);
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, _damageJumpSpeedPoins);

            if(_coins>0)
            {
             SapwnCoinsPoison();
            }
        }

        private void SapwnCoinsPoison()
        {
            var numCoinsToDispose = Mathf.Min(_coins, 1); // если у нас кол-во монет больше чем 1 то 1, если меньше, то все что осталось
            _coins -= numCoinsToDispose;
            Debug.Log(_coins);

            if (numCoinsToDispose > 0)
            {
                var burst = _hitParticalsePoins.emission.GetBurst(0);   // обращаемся к партикл системе, к чати где гогворим сколько выпускать, раздел - emissio, пункт - Burst
                burst.count = numCoinsToDispose;                        // устанавливаем кол-во партиклов которы енужно выкинуть
                _hitParticalsePoins.emission.SetBurst(0, burst);        //с сохраним это  в нашей притикл системе, раздел - emissio, пункт - Burst


                _hitParticalsePoins.gameObject.SetActive(true);// активируем пактикл систему ( по умолчанию она у нас выкл)
                _hitParticalsePoins.Play();// говорим ей что нужно проиграться
            }


        } 

        public void Interact()
        {
            Debug.Log("E");
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, _interactionRadius, _interactionResult, _interactionLayer); //OverlapCircleNonAlloc этот метод позволяет видеть пересекающиеся объекты и не выделять лишнюю память

        for (int i = 0; i < size; i++)
        {
            var interactable = _interactionResult[i].GetComponent<InteractableComponent>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
        }


        public void SpawnJumpDust()
        {
            _JumpParticles.Spawn();
        }

        public void CoinsScore1(int i)
        {

            if (i == 1)
            {
                _coins++;
                Debug.Log(_coins);
                i--;
            }
        }

        public void CoinsScore10(int j)
        {
            if (j == 1)
            {
                _coins += 10;
                Debug.Log(_coins);
                j--;
            }
        }

        public void SpawnFootDust()
        {
            _footParticles.Spawn();
        }

        private void OnCollisionEnter2D(Collision2D other) // стрим 13 (дз8)   ЧЕ ДА КАК СМОТРЕТЬ В СКРИПТЕ GameObjectExstensions
        {
            if (other.gameObject.IsInLayer(_groundlayer))
            {
                var contact = other.contacts[0];
                if (contact.relativeVelocity.y >= _fallVelocity)
                {
                    _fallPosition.Spawn();
                }
            }
        }
        public void Attack()
        {
           
            if (!_isArmed) return;
            _animator.SetTrigger(Attackk);

            

        }
        public void OnAttack()
        {
            var gos = _attackRange.GetObjectsInRange();
            foreach (var go in gos)
            {
                var hp = go.GetComponent<HealthComponent>();
                if (hp != null || go.CompareTag("Enemy"))
                {
                    Debug.Log("HideFlags,JNFTN NNFDGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG");
                    hp.ApplyDamage(_damage);
                }
            }
        }

        internal void ArmHero()
        {
            _isArmed = true;
            _animator.runtimeAnimatorController = _armed;
        }
    }
}
