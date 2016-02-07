using UnityEngine;
using System.Collections;
using Base.Audio; //Is required for audio.
using Base.Management; //Is required for ObjectPools.

public class AudioTest : MonoBehaviour {

    private ObjectPool clickPool;

	void Awake () {

        //We add the object to the pool and create its instances.
        clickPool = AudioManager.Instance.CreateAudioPoolInstance("Menu_Credits_Button_Click",10,true);

        //Parent it to the object for cleaner Hierarchy.
        clickPool.transform.parent = this.transform;



	}

    void FixedUpdate () {

        if (Input.GetKeyDown(KeyCode.Space)) {
            //Tries to play the Audio Object from the pool. the audio wont be played if the pool is empty.
            AudioObject audioObject = clickPool.GetObjectFromPool().GetComponent<AudioObject>();

            //Check if we got one or not, this isn't needed if the  _createsNewObject parameter is set to true.
            if (audioObject != null) {

                //Play the clip.
                audioObject.Play();

            }
        }

    }
	
}


