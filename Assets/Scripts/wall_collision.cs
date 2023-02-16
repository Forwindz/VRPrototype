using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_collision : MonoBehaviour
{
    public float openDownDistance = 6.0f;
    public float speed = 1.0f;
    protected float curOpenDownDistance = 0.0f;
    GameObject latern;
    GameObject wall;
    
    public bool collider = false;
        public AudioSource music;
    public AudioClip jump;//这里我要给主角添加跳跃的音效
	
	    private void Awake()
	{
		
        //给对象添加一个AudioSource组件
        music = gameObject.AddComponent<AudioSource>();
        //设置不一开始就播放音效
        music.playOnAwake = false;
        //加载音效文件，我把跳跃的音频文件命名为jump
        jump = Resources.Load<AudioClip>("music/wall");
    }

    // Start is called before the first frame update
    void Start()
    {
         latern = GameObject.Find("Lantern_thirdroom") ;
         wall = GameObject.Find("Block 7x5x7");
    }

    // Update is called once per frame
    void Update()

    {
        float dt = speed * Time.deltaTime;
        if(collider){
            if (curOpenDownDistance >= 6.0f)
                {
                    collider = false;
                    return;
                }
                dt *= openDownDistance - curOpenDownDistance;
                curOpenDownDistance += dt * 0.9f + dt * 0.1f;
                latern.transform.position += Vector3.up * dt;
                //wall.transform.position += Vector3.left * dt;
                //print(wall.transform.position);
        }
           

        
    }

     private void OnCollisionEnter(Collision collision)
    {
       
        print(this.name + "被" + collision.gameObject.name + "撞到了");
        collider = true;
            music.clip = jump;
                //播放音效
                music.Play();
       // print(latern.transform.position);


    }

}
