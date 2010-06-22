﻿using System.Collections.Generic;
using System;
using System.Drawing;

namespace Mood
{
    class World
    {
        public List<WorldObject> Objects { get; private set; }
        public List<IHitable> HitableObjects { get; private set; }
        public List<IShootable> ShootableObjects { get; private set; }

        public Laser lastLaser;

        public bool ShowAllLasers { get; set; }
        public bool ShowLastLaser { get; set; }

        public World()
        {
            Objects = new List<WorldObject>();
            HitableObjects = new List<IHitable>();
            ShootableObjects = new List<IShootable>();
            ShowAllLasers = false;
            ShowLastLaser = true;
            lastLaser = null;
        }

        public void AddObject(WorldObject obj)
        {
            Objects.Add(obj);

            if (obj is IHitable)
            {
                HitableObjects.Add(obj as IHitable);
            }

            if (obj is IShootable)
            {
                ShootableObjects.Add(obj as IShootable);
            }

            if (obj is Laser)
                lastLaser = obj as Laser;
        }

        public IHitable HitTest(IMoveable mvObj)
        {
            foreach (IHitable obj in HitableObjects)
            {
                if (obj != mvObj && obj.HitTest(mvObj))
                {
                    return obj;
                }
            }

            return null;
        }

        public void ShootTest(Laser line)
        {
            IShootable shot = null;
            double minDistance = 100d;

            foreach (IShootable obj in ShootableObjects)
            {
                if (obj.ShootTest(line) && obj.LastShootDistance < minDistance)
                {
                    shot = obj;
                }
            }

            if (shot != null)
            {
                ShootableObjects.Remove(shot);

                if (shot is IHitable)
                {
                    HitableObjects.Remove(shot as IHitable);
                }

                //line.SetRange((float)Geometry.PointDistance(shot.LastPosition(), line.B));

                line.B = shot.LastPosition();

                shot.Die();
            }
        }

        public void AddSphere()
        {
            Random random = new Random();

            AddObject(new Sphere(new Vector3d((float)random.NextDouble() * 6f - 3f, -0.8f, (float)random.NextDouble() * 6f - 3f), 0.5d, Color.Blue));
        }

        public void Draw()
        {
            foreach (WorldObject obj in Objects)
            {
                if (obj is Laser && ((lastLaser == obj && !ShowLastLaser) || (lastLaser != obj && !ShowAllLasers)))
                    continue;
                
                obj.Draw();
            }
        }
    }
}