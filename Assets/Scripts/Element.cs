using UnityEngine;

public class Element : MonoBehaviour {

	public bool Mine;

	[SerializeField]public Sprite[] EmptyTextures;
	[SerializeField]public Sprite MineTexture;
	private SpriteRenderer _spriteRenderer;

	private void Start ()
	{
		print(this.gameObject);
		_spriteRenderer = GetComponent<SpriteRenderer>();
		
		if (Random.value <= 0.15)
		{
			Mine = true;
		}

		int x = (int)transform.position.x;
		int y = (int)transform.position.y;
		Grid.Elements[x, y] = this;
	}
	
	public void LoadTexture(int adjacent)
	{
		_spriteRenderer.sprite = Mine ? MineTexture : EmptyTextures[adjacent];
	}
	
	public bool IsCovered() {
		return _spriteRenderer.sprite.texture.name == "default";
	}

	private void OnMouseUpAsButton() {
		if (Mine) {
			Grid.UncoverMines();

			print("Loser");
		}
		else {
			int x = (int)transform.position.x;
			int y = (int)transform.position.y;
			LoadTexture(Grid.AdjacentMines(x, y));
		}
		
		if (Grid.IsFinished()) print("Winner");

	}

}
