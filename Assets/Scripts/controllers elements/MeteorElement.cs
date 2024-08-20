using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MeteorElement : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private List<Sprite> _allMeteors;
    [SerializeField] private Image _image;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _modForce;

    #endregion Inspector variables

    #region Unity functions

    private void Start()
    {
        SetRandomSprite();
    }

    #endregion Unity functions

    #region public functions

    public void InitMeteor()
    {
        _rigidbody2D.AddForce(Vector2.down * _modForce);
        StartCoroutine(RoadToDisappear(Random.Range(5f, 20f))); //sad after all
    }

    #endregion public functions

    #region private functions

    private IEnumerator RoadToDisappear(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
    private void SetRandomSprite() => _image.sprite = _allMeteors[Random.Range(0, _allMeteors.Count)];

    #endregion private functions

}
