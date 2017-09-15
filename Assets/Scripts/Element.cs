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

		var x = (int)transform.localPosition.x;
		var y = (int)transform.localPosition.y;
		Grid.Elements[x, y] = gameObject;
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
			var x = (int)transform.localPosition.x;
			var y = (int)transform.localPosition.y;
			LoadTexture(Grid.AdjacentMines(x, y));
		}
		
		if (Grid.IsFinished()) print("Winner");

	}

}
