import { List } from "semantic-ui-react";
import SetWordsListItem from "./SetWordsListItem";
import PerfectScrollbar from 'react-perfect-scrollbar';
import 'react-perfect-scrollbar/dist/css/styles.css';
import { FlashCardWord } from "../../../../models/flashCards/flashCardWord";

interface Props {
    setId: string,
    isSetOwner: boolean,
    words: FlashCardWord[]
}

export default function SetWordsList({ setId, isSetOwner, words }: Props) {
    return (
        <PerfectScrollbar>
            <List verticalAlign='middle'>
                {words.map(word => {
                    return <SetWordsListItem 
                        setId={setId} 
                        isSetOwner={isSetOwner} 
                        word={word} 
                        key={word.id}
                    />
                })}
            </List>
        </PerfectScrollbar>
    );
}
