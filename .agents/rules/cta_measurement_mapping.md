# Rule: CTA Instrument Measurement Mapping Invariant

## Invariant
Never map `_instrument.GetMeasuredValues()` results directly to `DataGridView` rows using naive sequential index loops (`list[i] -> targetRowIndices[i]`).

## Reasoning
The CTA hardware instrument executes tests per **grouped wire net (connected component)** first, followed by component items (`CHECK COMPONENTS`). Therefore, the returned `MeasuredValueList` contains:
1. `list[0 .. G-1]`: Measurements for each of the $G$ connected wire net groups.
2. `list[G .. G+C-1]`: Measurements for each of the $C$ component items.

A harness with 14 grid rows might form only 2 wire groups and 2 components (total 4 instrument measurements).

## Implementation Requirement
Always group wire rows into connected components via Disjoint Set Union (DSU) matching `ConvertToProgramText`. Map group index $g$ to `list[g]` for all constituent rows in group $g$, and map component index $c$ to `list[G + c]` for component rows.
