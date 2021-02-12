using System;
using System.Collections.Generic;
using UnityEngine;
//using System.Numerics;
using System.Text;

namespace Test
{
    public class Tower
    {
        private Stack<GameObject> disks { get; } = new Stack<GameObject>(3);

        public int Height => disks.Count;
        public GameObject Top => disks.Peek();

        /*public float YOffset => Height switch
        {
            1 => 0.015f,
            2 => 0.025f,
            _ => 0,
        };

        */
        public float YOffset {
            get {
                switch (Height)
                {
                    case 1:
                        return 0.015f;
                    case 2:
                        return 0.025f;
                    default:
                        return 0;
                }
            }
        }

        public float ZOffset { get; set; }

        public Tower(float zOffset)
        {
            ZOffset = zOffset;
        }

        public bool IsWinFormation()
        {
            GameObject[] arr = disks.ToArray();
            return arr.Length == 3
                && arr[2].GetDiskType() == DiskType.Disk1
                && arr[1].GetDiskType() == DiskType.Disk2
                && arr[0].GetDiskType() == DiskType.Disk3;
        }

        public bool MoveDisk(Tower toTower)
        {
            
            if (Height == 0 || !Top.CanBeOn(toTower))
            {
                return false;
            }

            if (toTower.AddDisk(Top))
            {
                disks.Pop();
                return true;
            }
            return false;
        }

        public bool AddDisk(GameObject disk)
        {
            if (Height == 3)
                return false;

            disk.transform.localPosition = new Vector3(0, YOffset, ZOffset);
            disks.Push(disk);
            return true;
        }

        public void ClearTower()
        {
            disks.Clear();
        }
    }

    public enum DiskType
    {
        Disk1, Disk2, Disk3
    }

    public static class DiskExtensions {
        /*
        public static DiskType GetDiskType(this GameObject disk) => disk.name switch
        {
            "disk2" => DiskType.Disk2,
            "disk3" => DiskType.Disk3,
            _ => DiskType.Disk1
        };*/
        public static DiskType GetDiskType(this GameObject disk)
        {
            switch (disk.name)
            {
                case "disk2":
                    return DiskType.Disk2;
                case "disk3":
                    return DiskType.Disk3;
                default:
                    return DiskType.Disk1;
            }
            
        }


        public static bool CanBeOn(this GameObject disk, Tower tower)
        {
            //return (int)disk.GetDiskType() > (int)tower.Top.GetDiskType();
            return tower.Height == 0 || (int)disk.GetDiskType() > (int)tower.Top.GetDiskType();
        }
    }
}
