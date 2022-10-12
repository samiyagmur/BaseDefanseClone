using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{

    public class TurretStackGridController
    {

        private int _orderOfContayner ;
        private float _offset=0.5f;
        private int _xGridSize=2;
        private int _yGridSize=2;
        private int _maxContaynerAmount=40;
        private bool _contaynerFull;
        private Vector3 _lastPosition;
        private List<Vector3> _contaynerStackGridPositions= new List<Vector3>();


        public void GanarateGrid()
        {

            
            for (int i = 0; i < _maxContaynerAmount; i++)
            {
                if (_contaynerFull) return;


                var modx = _orderOfContayner % _xGridSize;

                var dividey = _orderOfContayner / _xGridSize;

                var mody = dividey % _yGridSize;

                var divideXY = _orderOfContayner / (_xGridSize * _yGridSize);

                _lastPosition = new Vector3(modx * _offset, divideXY * _offset, mody * _offset);//List place

                _contaynerStackGridPositions.Add(_lastPosition);

                if (_orderOfContayner == _maxContaynerAmount - 1)
                {
                    _contaynerFull = true;
                }
                else
                {
                    _contaynerFull = false;
                    _orderOfContayner += 1;
                } 
            }


        }

        public List<Vector3> LastPosition()
        {
            return _contaynerStackGridPositions;
        }


        
    }
}