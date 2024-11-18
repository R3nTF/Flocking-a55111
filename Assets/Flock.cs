using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Flock : MonoBehaviour
{
    float speed = 2;
    // Start is called before the first frame update
    void Start()
    {
       //este codigo não está a funcionar com o resto, ele não consegue pegar no valor e não sei porque, eu segui o que o tutorial mostrava. coloquei speed = 2 para pelo menos ter peicxes a mover, mas não percebo porque é que eles não se moviam usndo esta linha
       speed = Random.Range(FlockManager.FM.minSpeed, FlockManager.FM.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        ApplyRules();
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void ApplyRules()
    {
        //Aqui não há nada que eu consigo tratar. o problema persiste para a proxima parte. enquanto eu não saber como resolver isto, estou preso! ñada funciona apartir do painel novo que criamos na secção anterior
        //Eu não sei como prosseguir, estou preso.
        GameObject[] gos;
        gos = FlockManager.FM.allFish;

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.01f;
        float nDistance;
        int groupSize = 0;

        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if (nDistance <= FlockManager.FM.neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;

                    if(nDistance < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }
        if (groupSize > 0)
        {
            vcentre = vcentre / groupSize;
            speed = gSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), FlockManager.FM.rotationSpeed * Time.deltaTime);
        }
    }
}
