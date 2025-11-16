# Coinpusher Chronicle – Design Overview

## Core Systems

| System | Purpose |
| --- | --- |
| `CoinSpawner` | 物理的なコインを安定周期で落とす。音やランダム性も付与。 |
| `PusherController` | プッシャーの前後運動をサイン波で制御し、揺り戻しを演出。 |
| `CoinCollector` | コイン受け皿での得点計測とイベント転送。 |
| `EconomyManager` | プレイヤー残高、コイン購入、報酬支払い。 |
| `GameHUDController` | UI テキストとスライダーを更新。 |
| `ChronoVaultManager` | 独自ギミック。時間逆行ジャックポット。 |
| `ChronoVaultTrigger` | Vault へのコイン吸い込みスロット。 |
| `AutoDropController` | UI ボタンからドロップを呼び出す。 |

## Chrono Vault フロー

1. 側面トリガーにコインが落ちると `ChronoVaultTrigger` が `ChronoVaultManager` に報告。
2. Vault がチャージされ、HUD のゲージが上昇。
3. プレイヤーが任意のタイミングで "Rewind" ボタンを押すと `ChronoVaultManager.TriggerRewind()` を実行。
4. 蓄積量に応じて最大 `coinsPerBurst` 単位で連続的にコインが降り注ぐ。
5. `CoinCollector` で通常通り得点化されるが、降下位置がボード中央になるため大量連鎖が発生する。

## アート/技術メモ

- Coin プレハブは 64 トライアングル程度の円柱で十分。Physics Material で摩擦 0.2、反発 0.1 推奨。
- プッシャーと床には MeshCollider（Convex）を使い、Rigidbody は kinematic。
- Chrono Vault のエフェクトは VFX Graph で時間が巻き戻るような渦状パーティクルを推奨。
- パフォーマンスが気になる場合は GPU Instancing 対応のシェーダーを使用する。

## 参考タイムライン

| フェーズ | 期間 | 内容 |
| --- | --- | --- |
| Prototype | 2 週間 | コアプレイ・Chrono Vault 実装、物理調整。 |
| Vertical Slice | 4 週間 | アート統合、サウンド、UI 完成。 |
| Certification | 2 週間 | 量産筐体テスト、QA。 |

