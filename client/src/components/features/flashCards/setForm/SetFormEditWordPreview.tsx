import SetFormEditWord from "./SetFormEditWord";

interface Props {
    position: number,
    wordInputName: string,
    translationInputName: string
}

export default function SetFormEditWordPreview({position, wordInputName, translationInputName}: Props) {
    return <SetFormEditWord 
        position={position} 
        wordInputName={wordInputName} 
        translationInputName={translationInputName} 
        isDeleted={false} 
        onRemove={()=>{}}
        onUpdate={()=>{}}
    />   
}