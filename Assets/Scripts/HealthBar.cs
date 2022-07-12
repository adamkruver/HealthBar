using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _sliderUpdateTime = .1f;

    private Slider _slider; 
    private Coroutine _changeHealthJob;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _player.HealthChanged += ChangeHealth;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= ChangeHealth;
    }

    private void ChangeHealth(int health, int maxHealth)
    {
        float normalizedHealth = (float) health / maxHealth;

        ChangeSliderTo(normalizedHealth);
    }

    private void ChangeSliderTo(float destination)
    {
        if (_changeHealthJob != null)
            StopCoroutine(_changeHealthJob);

        _changeHealthJob = StartCoroutine(ChangeSliderValueCoroutine(destination));
    }

    private IEnumerator ChangeSliderValueCoroutine(float destination)
    {
        var awaiter = new WaitForFixedUpdate();

        while (_slider.value != destination)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, destination, Time.fixedDeltaTime * _sliderUpdateTime);

            yield return awaiter;
        }
    }
}
