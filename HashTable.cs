using System.Collections.ObjectModel;

namespace Solution;

public class HashTable<K, V> : IHashTable<K, V>
{
    Entry<K, V>[]? buckets { get; set;}

    public ReadOnlyCollection<Entry<K, V>> data => buckets == null? null : buckets.AsReadOnly();

    public HashTable() { buckets = null; }

    public HashTable(Entry<K, V>[]? input) { importData(input);}

    public HashTable(int capacity)
    {
        buckets = new Entry<K, V>[capacity];
    }

    protected int getIndex(K key)
    {
        int hashCode = Math.Abs(key.GetHashCode());
        return hashCode % buckets.Length;
    }

    public bool Add(K key, V value)
    {
        int index = getIndex(key);
        if (Find(key) != null) return false;

        if (buckets[index] == null){
            buckets[index] = new Entry<K, V>(key, value);
            return true;
        }

        var temp = (index+1) % buckets.Length;
        while (temp != index){
            if (buckets[temp] == null){
                buckets[temp] = new Entry<K, V>(key, value);
                return true;
            }
            temp = (temp+1) % buckets.Length;
        }
        return false;
    }

    public V? Find(K key)
    {
        int index = getIndex(key);
        if (buckets[index] != null && buckets[index].Key.Equals(key)){
            return buckets[index].Value;
        }

        var temp = (index+1) % buckets.Length;
        while (temp != index){
            if (buckets[temp] != null && buckets[temp].Key.Equals(key)){
                return buckets[temp].Value;
            }
            temp = (temp+1) % buckets.Length;
        }
        return default;
    }

    public bool Delete(K key)
    {
        throw new NotImplementedException();
    }


    //DO NOT REMOVE the following method:
    private void importData(Entry<K, V>[]? inputData){
        if(inputData != null) {
            buckets = new Entry<K, V>[inputData.Length];
            for (int i = 0; i < inputData.Length; ++i) 
                buckets[i] = inputData[i];
        }
    }
}
