using FluentAssertions;
using NUnit.Framework;
using RGV.Extensions.Runtime.Infrastructure;
using UnityEngine;

namespace RGV.Extensions.Tests.Editor
{
    public class UnityVectorExtensionsTests
    {
        #region With
        [Test]
        public void Vector3_With_MuteOnlySpecifiedCoords()
        {
            var sut = new Vector3(1, 1, 1);

            sut.With(x: 0).Should().Be(new Vector3(0, 1, 1));
            sut.With(y: 0).Should().Be(new Vector3(1, 0, 1));
            sut.With(z: 0).Should().Be(new Vector3(1, 1, 0));

            sut.With(x: 0, y: 0).Should().Be(new Vector3(0, 0, 1));
            sut.With(x: 0, z: 0).Should().Be(new Vector3(0, 1, 0));
            sut.With(y: 0, z: 0).Should().Be(new Vector3(1, 0, 0));

            sut.With(x: 0, y: 0, z: 0).Should().Be(Vector3.zero);
        }

        [Test]
        public void Vector3Int_With_MuteOnlySpecifiedCoords()
        {
            var sut = new Vector3Int(1, 1, 1);

            sut.With(x: 0).Should().Be(new Vector3Int(0, 1, 1));
            sut.With(y: 0).Should().Be(new Vector3Int(1, 0, 1));
            sut.With(z: 0).Should().Be(new Vector3Int(1, 1, 0));

            sut.With(x: 0, y: 0).Should().Be(new Vector3Int(0, 0, 1));
            sut.With(x: 0, z: 0).Should().Be(new Vector3Int(0, 1, 0));
            sut.With(y: 0, z: 0).Should().Be(new Vector3Int(1, 0, 0));

            sut.With(x: 0, y: 0, z: 0).Should().Be(Vector3Int.zero);
        }

        [Test]
        public void Vector2_With_MuteOnlySpecifiedCoords()
        {
            var sut = new Vector2(1, 1);

            sut.With(x: 0).Should().Be(new Vector2(0, 1));
            sut.With(y: 0).Should().Be(new Vector2(1, 0));

            sut.With(x: 0, y: 0).Should().Be(Vector2.zero);
        }

        [Test]
        public void Vector2Int_With_MuteOnlySpecifiedCoords()
        {
            var sut = new Vector2Int(1, 1);

            sut.With(x: 0).Should().Be(new Vector2Int(0, 1));
            sut.With(y: 0).Should().Be(new Vector2Int(1, 0));

            sut.With(x: 0, y: 0).Should().Be(Vector2Int.zero);
        }

        [Test]
        public void Color_With_MuteOnlySpecifiedFields()
        {
            var sut = new Color(1, 1, 1, 1);

            sut.With(r: 0).Should().Be(new Color(0, 1, 1, 1));
            sut.With(g: 0).Should().Be(new Color(1, 0, 1, 1));
            sut.With(b: 0).Should().Be(new Color(1, 1, 0, 1));
            sut.With(a: 0).Should().Be(new Color(1, 1, 1, 0));

            sut.With(r: 0, g: 0).Should().Be(new Color(0, 0, 1, 1));
            sut.With(r: 0, b: 0).Should().Be(new Color(0, 1, 0, 1));
            sut.With(g: 0, b: 0).Should().Be(new Color(1, 0, 0, 1));
            sut.With(r: 0, g: 0, b: 0).Should().Be(new Color(0, 0, 0, 1));

            sut.With(r: 0, g: 0, b: 0, a: 0).Should().Be(new Color(0, 0, 0, 0));
        }
        #endregion

        #region Add
        [Test]
        public void Vector3_Add_AddOnlySpecifiedCoords()
        {
            var sut = new Vector3(1, 1, 1);

            sut.Add(x: 2).Should().Be(new Vector3(3, 1, 1));
            sut.Add(y: 2).Should().Be(new Vector3(1, 3, 1));
            sut.Add(z: 2).Should().Be(new Vector3(1, 1, 3));

            sut.Add(x: 2, y: 2).Should().Be(new Vector3(3, 3, 1));
            sut.Add(x: 2, z: 2).Should().Be(new Vector3(3, 1, 3));
            sut.Add(y: 2, z: 2).Should().Be(new Vector3(1, 3, 3));

            sut.Add(x: 2, y: 2, z: 2).Should().Be(new Vector3(3, 3, 3));
        }

        [Test]
        public void Vector2_Add_AddOnlySpecifiedCoords()
        {
            var sut = new Vector2(1, 1);

            sut.Add(x: 2).Should().Be(new Vector2(3, 1));
            sut.Add(y: 2).Should().Be(new Vector2(1, 3));

            sut.Add(x: 2, y: 2).Should().Be(new Vector2(3, 3));
        }

        [Test]
        public void Vector3Int_Add_AddOnlySpecifiedCoords()
        {
            var sut = new Vector3Int(1, 1, 1);

            sut.Add(x: 2).Should().Be(new Vector3Int(3, 1, 1));
            sut.Add(y: 2).Should().Be(new Vector3Int(1, 3, 1));
            sut.Add(z: 2).Should().Be(new Vector3Int(1, 1, 3));

            sut.Add(x: 2, y: 2).Should().Be(new Vector3Int(3, 3, 1));
            sut.Add(x: 2, z: 2).Should().Be(new Vector3Int(3, 1, 3));
            sut.Add(y: 2, z: 2).Should().Be(new Vector3Int(1, 3, 3));

            sut.Add(x: 2, y: 2, z: 2).Should().Be(new Vector3Int(3, 3, 3));
        }

        [Test]
        public void Vector2Int_Add_AddOnlySpecifiedCoords()
        {
            var sut = new Vector2Int(1, 1);

            sut.Add(x: 2).Should().Be(new Vector2Int(3, 1));
            sut.Add(y: 2).Should().Be(new Vector2Int(1, 3));

            sut.Add(x: 2, y: 2).Should().Be(new Vector2Int(3, 3));
        }

        [Test]
        public void Vector_Add_AlsoWorks_WithNegativeArgs()
        {
            var sut = new Vector3(1, 1, 1);

            sut.Add(x: -2).Should().Be(new Vector3(-1, 1, 1));
            sut.Add(y: -2).Should().Be(new Vector3(1, -1, 1));
            sut.Add(z: -2).Should().Be(new Vector3(1, 1, -1));

            sut.Add(x: -2, y: -2).Should().Be(new Vector3(-1, -1, 1));
            sut.Add(x: -2, z: -2).Should().Be(new Vector3(-1, 1, -1));
            sut.Add(y: -2, z: -2).Should().Be(new Vector3(1, -1, -1));

            sut.Add(x: -2, y: -2, z: -2).Should().Be(new Vector3(-1, -1, -1));
        }
        #endregion
    }
}