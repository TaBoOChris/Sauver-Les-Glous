using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance { get; private set; }

	[Header("Pistes audio")]
	[SerializeField] private AudioSource m_musicSource;
	[SerializeField] private AudioSource m_sfxSource;

	[Header("Music")]
	[SerializeField] public AudioClip m_menuMusic;
	[SerializeField] public AudioClip m_gameMusic;
	[SerializeField] public AudioClip m_villageMusic;

	[Header("Menu")]
	[SerializeField] public AudioClip m_buttonSfx;

	[Header("Glou")]
	[SerializeField] public AudioClip m_GlouSfx;
	[SerializeField] public AudioClip m_GlouSfxCute;
	[SerializeField] public AudioClip m_GlouSpawnSfx;
	[SerializeField] public AudioClip m_GlouDieSfx;

	private void Awake()
	{
		if (Instance != null)
		{
			Debug.LogWarning("Il y a plus d'une instance de AudioManager dans la scene");
			return;
		}
		Instance = this;
	}
	void Start()
	{
		PlayMenuMusic();
	}
	void Update()
	{/* --quick fix de merde
		if (!m_musicSource.isPlaying)
		{
			m_musicSource.Play();
		}*/
	}

    #region Menu Music
    public void PlayMenuMusic()
	{
		m_musicSource.clip = m_menuMusic;
		m_musicSource.Play();
	}
	public void StopMusic()
    {
		m_musicSource.Stop();
		m_sfxSource.Stop();

	}
	public void PlayGameMusic()
	{
		m_musicSource.Stop();
		m_musicSource.clip = m_gameMusic;
		m_musicSource.Play();
	}

	public void PlayVillageMusic()
	{
		m_musicSource.Stop();
		m_musicSource.clip = m_villageMusic;
		m_musicSource.Play();
	}
	#endregion

	#region Menu Sfx
	public void PlayButtonMenu()
	{
		m_sfxSource.PlayOneShot(m_buttonSfx);
	}
    #endregion

    #region Glou Sfx
    public void PlayGlou()
	{
		m_sfxSource.PlayOneShot(m_GlouSfx);
	}
	public void PlayGlouCute()
	{
		m_sfxSource.PlayOneShot(m_GlouSfxCute);
	}
	public void PlayGlouSpawn()
	{
		m_sfxSource.PlayOneShot(m_GlouSpawnSfx);
	}

	public void PlayGlouDie()
    {
		m_sfxSource.PlayOneShot(m_GlouDieSfx);
	}
    #endregion


}
