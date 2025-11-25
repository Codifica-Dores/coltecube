from pathlib import Path

pasta = Path(r".")
txt = ["#begin ./",0,
"""\n/importer:TextureImporter
/processor:TextureProcessor
/processorParam:ColorKeyColor=255,0,255,255
/processorParam:ColorKeyEnabled=True
/processorParam:GenerateMipmaps=False
/processorParam:PremultiplyAlpha=True
/processorParam:ResizeToPowerOfTwo=False
/processorParam:MakeSquare=False
/processorParam:TextureFormat=Color
/build:./""",0,"\n\n"]
all = []
arq = "put_on_mgcb.txt"
for arquivo in pasta.rglob("*"):
    if arquivo.is_file():
        if arquivo.suffix.lower() in [".png", ".jpg", ".jpeg"]:
            rel = arquivo.relative_to(pasta)      # <<< A ÚNICA COISA ADICIONADA

            cp = txt
            cp[1] = str(rel)                      # <<< USANDO O CAMINHO RELATIVO
            cp[3] = str(rel)                      # <<< IGUAL AO SEU PADRÃO
            takaLa = ""
            for a in cp:
                takaLa += ""+a
            all.append(takaLa)
            
with open(arq, "w", encoding="utf-8") as f:
    f.writelines(all)
