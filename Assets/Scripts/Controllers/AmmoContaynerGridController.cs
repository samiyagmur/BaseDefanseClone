using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class AmmoContaynerGridController :IGridAble
    {

        private int _orderOfContayner ;
        private float _offset;
        private int _xGridSize;
        private int _yGridSize;
        private int _maxContaynerAmount;
        private bool _contaynerFull;
        private Vector3 _lastPosition;

        public AmmoContaynerGridController( int xGridSize, int yGridSize, int maxContaynerAmount, float offset)
        {
            
            _xGridSize = xGridSize;
            _yGridSize = yGridSize;
            _maxContaynerAmount = maxContaynerAmount;
            _offset = offset;
        }

        public void ganarateGrid()
        {

            if (!_contaynerFull) return ;


                var modx = _orderOfContayner % _xGridSize;

                var dividey = _orderOfContayner / _xGridSize;

                var mody = dividey % _yGridSize;

                var divideXY = _orderOfContayner / (_xGridSize * _yGridSize);

                _lastPosition = new Vector3(modx * _offset, divideXY * _offset, mody * _offset);//List place

                

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

        public Vector3 LastPosition()
        {
            return _lastPosition;
        }


        
    }
}