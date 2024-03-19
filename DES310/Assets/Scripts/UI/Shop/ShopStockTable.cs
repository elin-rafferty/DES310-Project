using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Shop Stock Table", menuName = "Data/Shop Stock Table")]
public class ShopStockTable : ScriptableObject
{
    public int itemsToReturn = 3;
    public List<ShopOffer> offers = new();
    bool allowDuplicates = false;

    public List<ShopOffer> GenerateOffers()
    {
        List<ShopOffer> chosenOffers = new();
        List<int> offerIndexes = new();
        // Check offers have been set up correctly
        if ((offers.Count < itemsToReturn && !allowDuplicates) || (offers.Count == 0))
        {
            Debug.Log("Shop stock table not set up correctly");
            return chosenOffers;
        }
        for (int rolls = 0; rolls < itemsToReturn; rolls++)
        {
            int totalWeights = 0;
            // Create and fill list of weightings for each offer with upper and lower bounds
            List<Tuple<int, int>> weightingRanges = new List<Tuple<int, int>>();
            foreach (var offer in offers)
            {
                weightingRanges.Add(new Tuple<int, int>(totalWeights, totalWeights + offer.weighting));
                totalWeights += offer.weighting;
            }
            // Choose a random number within total weighting range
            int random = UnityEngine.Random.Range(0, totalWeights);
            // Find where that random number landed
            for (int i = 0; i < weightingRanges.Count; i++)
            {
                if (random >= weightingRanges[i].Item1 && random < weightingRanges[i].Item2)
                {
                    if (offerIndexes.Contains(i) && !allowDuplicates)
                    {
                        rolls--;
                    } else
                    {
                        offerIndexes.Add(i);
                    }
                }
            }
        }
        foreach (int offer in offerIndexes)
        {
            chosenOffers.Add(offers[offer]);
        }
        return chosenOffers;
    }
}
