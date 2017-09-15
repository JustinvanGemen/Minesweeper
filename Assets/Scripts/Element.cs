using UnityEngine;

public class Element : MonoBehaviour {

	public bool Mine;

	[SerializeField]public Sprite[] EmptyTextures;
	[SerializeField]public Sprite MineTexture;
	private SpriteRenderer _spriteRenderer;

	private void Start ()
	{
		
		_spriteRenderer = GetComponent<SpriteRenderer>();
		
		if (Random.value <= 0.15) Mine = true;

		var x = (int)transform.position.x;
		var y = (int)transform.position.y;
		Grid.Elements[x, y] = gameObject;
		Grid.VisitedCreate();
	}
	
	public void UpdateSprite(int adjacent)
	{
		_spriteRenderer.sprite = Mine ? MineTexture : EmptyTextures[adjacent];
	}
	
	public bool IsCovered() {
		return _spriteRenderer.sprite.texture.name == "default";
	}

	private void OnMouseUpAsButton() {
		if (Mine) {
			Grid.ShowMines();
		}
		else {
			
			int x = (int)transform.position.x;
			int y = (int)transform.position.y;
			UpdateSprite(Grid.MinesNearby(x, y));
			Grid.FunKoffer(x, y);
		}

		if (Grid.IsFinished())
		{
			//ToDo: Game End.
		}

	}

}
