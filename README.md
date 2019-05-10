# ProcedualSkydomeGenerator

Skydomeのテクスチャ生成を目的としたパッケージです。
生成したテクスチャはその他の用途にもお使いいただけます。

## ダウンロード
<a target="blank" href="https://github.com/NekomimiMaster/ProcedualSkydomeGenerator/releases">ProcedualSkydomeGenerator/releases</a> より、unitypackageをダウンロードしてください。

## 使い方
1.ProcedualSkydomeGenerator_xxx.unitypackage をダウンロードしてください。<br>
2.Skydomeを追加したいプロジェクトを開いた状態でパッケージを開いてインポートしてください。<br>
3.SceneフォルダにあるMakingSkydomeのシーンを開いて実行。<br>
4.UIでskydomeの設定を変更後、右上のボタンにあるテクスチャを生成するを押すと、<br>
SimpleSkydomeGenerator/generateTex/ の中にskydomeのテクスチャが生成されます。<br>
5.スカイドーム用Materialを新規作成。シェーダーを UniGLTF/textureに設定し、 生成したテクスチャを割り当てます。<br>
6.model/の中にあるSimpleSkydomeに5のマテリアルをアタッチ<br>
→ unlitのskydomeができます。<br>

## ライセンス
MIT

## 作者

ねこみみますたー / https://twitter.com/kemomimi_oukoku
