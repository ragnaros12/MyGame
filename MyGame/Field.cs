using System;

namespace MyGame;

public class Field
{
    private Item[,] _items;
    private int n;
    
    public delegate void _updateListener(int row, int column, Item item);

    private _updateListener update;

    public Field(int n, _updateListener _update)
    {
        update = _update;
        this.n = n;
        _items = new Item[n,n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                _items[i, j] = new Item();
            }
        }
        Generate();
    }

    public Item Get(int row, int column)
    {
        return _items[row, column];
    }
    public void Rotate(int row, int column)
    {
        _items[row, column].Rotate();
        update(row, column, _items[row,column]);
        for (int i = 0; i < n; i++)
        {
            _items[row, i].Rotate();
            update(row, i, _items[row,i]);

            _items[i, column].Rotate();
            update(i, column, _items[i, column]);
        }
    }

    public bool Check()
    {
        Status status = _items[0, 0].Status;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (status != _items[i, j].Status)
                    return false;
            }
        }

        return true;
    }

    private void Generate()
    {
        int rnd = Random.Shared.Next(10);
        for (int i = 0; i < rnd; i++)
            Rotate(Random.Shared.Next(n), Random.Shared.Next(n));

        if (Check())
            Generate();
    }
}