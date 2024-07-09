using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] ParticleSystem winvfx1;
    [SerializeField] ParticleSystem winvfx2;

    [SerializeField] GameObject chest_open;
    [SerializeField] GameObject chest_close;

    private void Update()
    {
        Finish();
    }

    void Finish()
    {
        Physics.Raycast(transform.position + offset, Vector3.back,out RaycastHit hit ,.6f, playerLayer);
        if(hit.collider != null)
        {
            Player player = hit.collider.gameObject.GetComponent<Player>();

            winvfx1.Play();
            winvfx2.Play();

            chest_close.SetActive(false);
            chest_open.SetActive(true);

            Vector3 pos = chest_open.transform.position + new Vector3(0, -.8f, -1);

            player.finish = true;
            player.OnDespawn();
            player.transform.position = pos;
            player.ChangeAnim(Constants.ANIM_WIN);

            Invoke(nameof(UpdateGameState), 2f);
        }
    }

    void UpdateGameState()
    {
        GameManager.Ins.ChangeState(GameState.Finish);
    }
}
