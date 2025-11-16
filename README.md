# Coinpusher Chronicle

Unity 2022 LTS 向けに構築したハイエンドなコインプッシャー。リアルな物理挙動、カスタムオーディオ、Chrono Vault（時間逆行型ジャックポット）を搭載しており、アーケード筐体として市販できる完成度を目指しました。

## 機能ハイライト

- **実写感のあるプレイフィールド**: メッシュコライダーと高密度の rigidbody コインで構築。
- **柔らかな押し出し制御**: `PusherController` が滑らかなサイン波モーションを実現。
- **経済＆UI レイヤー**: `EconomyManager` と `GameHUDController` が残高とチャージ状態を HUD に反映。
- **Chrono Vault (独自ギミック)**: 側面の吸い込み口で集めたコインを時間逆行させ、ボード上空から隕石のように降らせるジャックポット。蓄積と解放をプレイヤーがタイミングよく行えるため、従来のコインプッシャーにはない戦略性が生まれます。

## Unity プロジェクト構成

```
Assets/
  Prefabs/
  Scripts/
Docs/
```

主要スクリプトは `Assets/Scripts` に配置されています。必要に応じて `Prefabs` にコイン、プッシャー、UI などを保存してください。

## セットアップ手順

1. Unity Hub で **Unity 2022.3 LTS**（HDRP/URP どちらでも可）をインストールします。
2. このリポジトリを新規プロジェクトフォルダとして開きます。
3. `Assets/Prefabs` に以下のようなプレハブを作成し、シーンに配置します。
   - `Coin`（Rigidbody + MeshCollider + Tag="Coin"）。
   - `Pusher`（`PusherController` をアタッチ）。
   - `Dropper`（`CoinSpawner`、`AutoDropController`）。
   - `CollectionTray`（`CoinCollector`）。
   - `Chrono Vault`（後述）。
4. Canvas 上に残高テキストとスライダーを置き、`GameHUDController` とイベントを紐付けます。
5. `EconomyManager` の `onBalanceChanged` を HUD に、`CoinCollector` の `onCoinsCollected` を `EconomyManager.OnCoinCollected` に接続します。

## Chrono Vault の組み込み

1. フィールド側面にトリガー付きのスロットを設置し、`ChronoVaultTrigger` をアタッチします。`vaultManager` 参照を設定してください。
2. 任意の VFX/Particle System を作成し、`ChronoVaultManager` の `rewindFx` に設定します。
3. `ChronoVaultManager` の `onChargeChanged` を HUD スライダーへ接続します。
4. プレイヤー操作 UI（ボタン、レバー等）から `ChronoVaultManager.TriggerRewind()` を呼ぶと、蓄積されたコインが上空から時間を巻き戻す演出で降り注ぎます。

## シーンおよびアートに関する推奨事項

- 1 メートル = 1 ユニットのスケールでモデル化すると、PhysX の安定性が高まります。
- 物理マテリアルで摩擦・弾性を調整し、コインが自然に滑るようにします。
- サウンドデザインには複数の衝突音を用意して `AudioSource.PlayOneShot` でランダム再生すると臨場感が増します。

## ライセンス

商用制作を想定し、著作権は本プロジェクトの制作者に帰属します。外部アセットを追加する場合はそれぞれのライセンスに従ってください。
