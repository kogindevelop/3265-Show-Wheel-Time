using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    [ExecuteAlways]
    public class GridLayoutFixedGroup : LayoutGroup
    {
        public enum ConstraintMode { FixedColumns, FixedRows }
        public enum AlignmentMode { Start, Center, End }

        [Header("Grid Constraint")]
        [SerializeField] private ConstraintMode _constraintMode = ConstraintMode.FixedColumns;
        [SerializeField, Min(1)] private int _fixedCount = 2;

        [Header("Spacing")]
        [SerializeField, Range(0f, 0.2f)] private float _spacingPercent = 0.02f;

        [Header("Padding (as percent of parent)")]
        [SerializeField, Range(0f, 0.5f)] private float _leftPercent = 0.05f;
        [SerializeField, Range(0f, 0.5f)] private float _rightPercent = 0.05f;
        [SerializeField, Range(0f, 0.5f)] private float _topPercent = 0.05f;
        [SerializeField, Range(0f, 0.5f)] private float _bottomPercent = 0.05f;

        [Header("Alignment for Incomplete Rows/Columns")]
        [SerializeField] private AlignmentMode _alignment = AlignmentMode.Center;

        private bool IsColumnBased => _constraintMode == ConstraintMode.FixedColumns;

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            ArrangeChildren();
        }

        public override void CalculateLayoutInputVertical() => ArrangeChildren();
        public override void SetLayoutHorizontal() => ArrangeChildren();
        public override void SetLayoutVertical() => ArrangeChildren();

        private void ArrangeChildren()
        {
            int childCount = rectChildren.Count;
            if (childCount == 0) return;

            ConfigureRectTransform();

            float spacingX = rectTransform.rect.width * _spacingPercent;
            float spacingY = rectTransform.rect.height * _spacingPercent;

            int columns = CalculateColumnCount(childCount);
            int rows = CalculateRowCount(childCount, columns);

            float[] columnWidths = CalculateCellSizes(columns, rectTransform.rect.width, _leftPercent, _rightPercent, spacingX);
            float[] rowHeights = CalculateCellSizes(rows, rectTransform.rect.height, _topPercent, _bottomPercent, spacingY);

            if (IsColumnBased)
                ArrangeByRows(columns, rows, columnWidths, rowHeights, spacingX, spacingY);
            else
                ArrangeByColumns(columns, rows, columnWidths, rowHeights, spacingX, spacingY);
        }

        private void ConfigureRectTransform()
        {
            rectTransform.pivot = new Vector2(0.5f, 1f);
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
        }

        private int CalculateColumnCount(int childCount)
        {
            return IsColumnBased ? _fixedCount : Mathf.CeilToInt((float)childCount / _fixedCount);
        }

        private int CalculateRowCount(int childCount, int columnCount)
        {
            return IsColumnBased ? Mathf.CeilToInt((float)childCount / columnCount) : _fixedCount;
        }

        private float[] CalculateCellSizes(int count, float parentSize, float startPercent, float endPercent, float spacing)
        {
            float available = parentSize * (1f - startPercent - endPercent) - (count - 1) * spacing;
            float size = available / count;
            return Enumerable.Repeat(size, count).ToArray();
        }

        private void ArrangeByRows(int columns, int rows, float[] columnWidths, float[] rowHeights, float spacingX, float spacingY)
        {
            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;

            float xOffset = padding.left + parentWidth * _leftPercent;
            float yOffset = -padding.top - parentHeight * _topPercent;

            for (int row = 0; row < rows; row++)
            {
                int itemsInRow = (row == rows - 1)
                    ? Mathf.Min(rectChildren.Count - row * columns, columns)
                    : columns;

                float alignOffset = GetAlignmentOffset(columns, itemsInRow, columnWidths[0] + spacingX);

                for (int col = 0; col < itemsInRow; col++)
                {
                    int index = row * columns + col;
                    if (index >= rectChildren.Count) break;

                    var child = rectChildren[index];

                    float xPos = xOffset + alignOffset + columnWidths.Take(col).Sum() + col * spacingX;
                    float yPos = yOffset - row * (rowHeights[row] + spacingY);

                    SetChildAlongAxis(child, 0, xPos, columnWidths[col]);
                    SetChildAlongAxis(child, 1, -yPos, rowHeights[row]);
                }
            }
        }

        private void ArrangeByColumns(int columns, int rows, float[] columnWidths, float[] rowHeights, float spacingX, float spacingY)
        {
            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;

            float xOffset = padding.left + parentWidth * _leftPercent;
            float yOffset = -padding.top - parentHeight * _topPercent;

            for (int col = 0; col < columns; col++)
            {
                int itemsInCol = (col == columns - 1)
                    ? Mathf.Min(rectChildren.Count - col * rows, rows)
                    : rows;

                float alignOffset = GetAlignmentOffset(rows, itemsInCol, rowHeights[0] + spacingY);

                for (int row = 0; row < itemsInCol; row++)
                {
                    int index = col * rows + row;
                    if (index >= rectChildren.Count) break;

                    var child = rectChildren[index];

                    float xPos = xOffset + col * (columnWidths[col] + spacingX);
                    float yPos = yOffset - alignOffset - row * (rowHeights[row] + spacingY);

                    SetChildAlongAxis(child, 0, xPos, columnWidths[col]);
                    SetChildAlongAxis(child, 1, -yPos, rowHeights[row]);
                }
            }
        }

        private float GetAlignmentOffset(int total, int actual, float cellWithSpacing)
        {
            int missing = total - actual;
            return _alignment switch
            {
                AlignmentMode.Center => missing * cellWithSpacing / 2f,
                AlignmentMode.End => missing * cellWithSpacing,
                _ => 0f
            };
        }
    }
}
