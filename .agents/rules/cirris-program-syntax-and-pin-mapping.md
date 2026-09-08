# Rule: Cirris Program Syntax and Pin Mapping Invariants

## 1. Backward Compatibility Invariant
When modifying or extending `TestConfigService`:
- Never delete or remove support for legacy Cirris 1100 test programs.
- The legacy parser must remain accessible as `ConvertToTestConfig1100(string configText)`.
- `ConvertToTestConfig(string configText)` must automatically inspect input text and route legacy syntax (starting with `(`, `*`, or containing `** TEST METHOD`) to `ConvertToTestConfig1100`.

## 2. Pin Notation & Formatting Invariant
- For Cirris 8100 export text (`ConvertToProgramText`), format pins strictly as `J{conn}-{pin}` without zero padding and without leading `$` (e.g. `J1-1`, `J4-1`).
- For pin parsing (`ParsePin`), tolerate both standard `J{conn}-{pin}` and legacy/export `$J{conn}-{pin}` formats.
- Pin numbering is 1-indexed (channels 1 to 448 mapped across connectors `J1` to `J7`, 64 pins per connector).

## 3. Unit Multipliers & Tolerances
- Numerical values parsed from `PARAMETERS` or `INSTRUCTIONS` may include SI prefixes (`k`, `M`, `m`, `u`, `µ`, `n`, `p`).
- Tolerances must be parsed and stored as numerical percentages without throwing exceptions whether `%` is present or omitted.
