using UnityEngine;
using System.Collections;
using System;



/// <summary>
/// SONG CLASS: TRACKS AN AUDIO CLIP GIVEN THE BPM TO KEEP TRACK OF EVERYTHING THAT'S GOING ON
/// </summary>
[Serializable]
public class Song
{
    [SerializeField] AudioClip song;
    [SerializeField] float BPM; //Beats per minute of the current song


    public enum Passed
    {
        NONE,
        EIGHTH,
        BEAT,
        MEASURE
    }


    float currentTimer; //get the current timerof the song between eights
    float eighthTimer = converter / 150f; //how long does it take for 1 eighth note
    int measureNumber;
    int beatNumber;
    int eighthNumber;
    int[] measureList = new int[3] {0, 0, 0}; // tracker in [measures, beats, eighths]
    const float converter = 30f; //converts BPM to eighth timer;

    public Song(AudioClip s, float bpm)
    {
        setSong(s, bpm);
    }

    public AudioClip getSong() { return song; }
    public float getBPM() { return BPM; } 
    public float getEighthTimer() { return eighthTimer; }
    public int getMeasureNumber() { return measureNumber; }
    public int getBeatNumber() { return beatNumber; }
    public int getTotalBeats() { return getMeasureNumber() * 4 + getBeatNumber(); }
    public int getEighthNumber() { return eighthNumber; }
    public float getRemainderTimer() { return currentTimer; }
    public int[] getMeasureTotal() { return new int[3] { getMeasureNumber(), getBeatNumber(), getEighthNumber() }; }

    public float getTimeUntil(int m, int b, int e)
    {
        while (e > 1)
        {
            e -= 2;
            b += 1;
        }
        while (b > 3)
        {
            b -= 4;
            m += 1;
        }
        int[] k = new int[3]
        {
            m, b, e
        };

        int[] j = new int[3]
        {
            getMeasureNumber(), getBeatNumber(), getEighthNumber()
        };

        for (var i = 0; i < 3; i++)
        {
            k[i] -= j[i];
            if (k[i] < 0) return -1f;
        }
        return getEighthTimer() * (k[0] * 4 * 2 + k[1] * 2 + k[2]);
    }


    public void setMeasureNumber(int n) { measureNumber = n; }
    public void setBeatNumber(int n) { beatNumber = n; }
    public void setEighthNumber(int n) { eighthNumber = n; }
    public void setSong(AudioClip s, float bpm)
    {
        if (s == null) return;
        song = s;
        setBpm(bpm);
    }

    public void resetSong()
    {
        eighthTimer = converter / getBPM();
        currentTimer = eighthTimer;
        measureNumber = 0;
        beatNumber = 0;
        eighthNumber = 0;
        resetTimer();
    }

    public void setBpm(float bpm)
    {
        if (bpm <= 0f)
            throw new Exception("Exception: 0bpm shown");
        else
        {
            BPM = bpm;
            eighthTimer = converter / BPM;
            resetTimer();
        }
    }

    protected Passed updateSongPosition()
    {
        Passed a = Passed.NONE;
        if (getEighthNumber() > 1)
        {
            setBeatNumber(getBeatNumber() + 1);
            setEighthNumber(getEighthNumber() -  2);
            a = Passed.BEAT;
        }
        if (getBeatNumber() > 3)
        {
            //Debug.Log("what");
            setMeasureNumber(getMeasureNumber() + 1);
            setBeatNumber(getBeatNumber() - 4);
            a = Passed.MEASURE;
        }
        return a;
    }
    protected void resetTimer()
    {
        currentTimer = eighthTimer;
    }
    public Passed update() //return true if an eighth note passes
    {
        currentTimer -= Time.deltaTime;
        //Debug.Log(getTimeUntil(4, 1, 1));
        if (currentTimer <= 0)
        {
            currentTimer = getEighthTimer() + currentTimer;
            setEighthNumber(getEighthNumber() + 1);
            Passed a = updateSongPosition();
            if (a == Passed.NONE)
                return Passed.EIGHTH;
            return a;
        }
        return Passed.NONE;
    }
}

public class Instrument : Boss {
    AudioSource source;

    [SerializeField] protected Song s;

    private int counterList;

    // Use this for initialization
    void Start()
    {
        counterList = 0;
        reset();
        startHealthbar();
        setSource();
        source.clip = s.getSong();
        source.Play();
        s.resetSong();
    }

    // Update is called once per frame
    void Update()
    {
        attack();
#if false
        if (s.update() >= Song.Passed.BEAT)
        {
            if (s.getBeatNumber() % 2 == 0)
            {
                if (getBulletList(0).getShooter() == null)
                    getBulletList(0).reset();
                getBulletList(0).getShooter().shootCircle(getBulletList(0).getBulletSpeed(), transform.position, 15);
            }
        }
#endif
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Bullet")
            damage(1);
    }

    public void setSource()
    {
        source = gameObject.GetComponent<AudioSource>();
        if (source == null)
            source = gameObject.AddComponent<AudioSource>();
    }

    public float getAngleBetween(GameObject g)
    {
        if (g == null) return 0;
        return Vector2.Angle(transform.position, g.transform.position);
    }

    public override void attack()
    {
        if (counterList == 0)
            getBulletList(0).shoot(BulletList.ShotType.CIRCLE, transform.position, getAngleBetween(GameObject.FindGameObjectWithTag("Player")), 4);
        //throw new NotImplementedException();
    }

    public override void move()
    {
        throw new NotImplementedException();
    }

    public override void ready()
    {
        throw new NotImplementedException();
    }

    protected override void afterLife()
    {
        Debug.Log(getHp());
        //Destroy(gameObject);
    }

}
