﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFO
{
    public class DiskController
    {
        public Color[] Colors = { Color.black, Color.blue, Color.cyan, Color.green, Color.grey, Color.red, Color.yellow };
        private float[] speeds = { 1, 1.1f, 1.4f };
        GameObject disk;
        Vector3 emissionPositon;
        Vector3 emissionDiretion;
        Vector3 force;
        public DiskController()
        {
            emissionPositon = new Vector3(1.5f, 4.2f, -10f);
            emissionDiretion = new Vector3(5.5f, 13.0f, 14f);
        }

        public void fireDisk(Disk.DiskLevel level)
        {
            var factory = DiskFactory.getInstance();
            disk = factory.getDisk(level);
            var diskScale = Random.Range(1, 3);
            disk.transform.localScale *= diskScale;
            int chooseColor = Random.Range(0, 7);
            disk.GetComponent<Renderer>().material.color = Colors[chooseColor];
            disk.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), emissionPositon.y, emissionPositon.z);
            disk.transform.rotation = Quaternion.identity;
            emissionDiretion.x = emissionDiretion.x * Random.Range(-1, 1);
            disk.SetActive(true);
            force = emissionDiretion * Random.Range(1.0f, 1.1f)*speeds[(int)level]*disk.GetComponent<Disk>().speed / 150;
            disk.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }

        public bool checkHitGround()
        {
            return disk.transform.position.y < 0;
        }

        public void freeDisk()
        {
            var factory = DiskFactory.getInstance();
            disk.GetComponent<Rigidbody>().velocity = Vector3.zero;
            disk.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            disk.SetActive(false);
            factory.freeDisk(disk);
        }

        public GameObject getGameObject()
        {
            return disk;
        }

        public int getScore()
        {
            return disk.GetComponent<Disk>().score;
        }
    }
}