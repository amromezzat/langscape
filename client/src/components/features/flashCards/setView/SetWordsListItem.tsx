import { Button, Grid, Icon, List, Segment } from "semantic-ui-react";
import '../../../../styles/SetWordsList.css'
import { FlashCardWord } from "../../../../models/flashCards/flashCardWord";
import { useState } from "react";
import { useStore } from "../../../../stores/core/store";
import SetWordsListWordInput from "./form/SetWordsListWordInput";
import '../../../../styles/Common.css';

interface Props {
    setId: string,
    isSetOwner: boolean,
    word: FlashCardWord
}

export default function SetWordsListItem({ setId, isSetOwner, word }: Props) {
    const [isEditing, setIsEditing] = useState(false);
    const {flashCardStore: { updateWord }} = useStore();
    const [modifiedWord, setModifiedWord] = useState(word.word);
    const [modifiedTranslation, setModifiedTranslation] = useState(word.translation);

    function handleClick() {
        if(isEditing) {
            word.word = modifiedWord;
            word.translation = modifiedTranslation;
            updateWord(setId, word);
        }
        setIsEditing(!isEditing);
    }

    return (
        <List.Item className='set-words-list-item'>
            <List.Content>
                <Segment textAlign='center'>
                    <Grid columns={'equal'} verticalAlign='middle'>
                        <Grid.Column float='left' >
                            { isEditing ? 
                                <SetWordsListWordInput 
                                    initial={word.word}
                                    setWord={setModifiedWord}
                                /> : 
                                word.word 
                            }
                        </Grid.Column>
                        <Grid.Column> | </Grid.Column>
                        <Grid.Column textAlign='left'>
                            { isEditing ? 
                                <SetWordsListWordInput 
                                    initial={word.translation}
                                    setWord={setModifiedTranslation}
                                /> : 
                                word.translation 
                            }
                        </Grid.Column>
                        {isSetOwner &&
                            <Grid.Column textAlign='right'>
                                <Button
                                    onClick={handleClick}
                                    basic
                                    compact
                                    className='borderless-button'
                                >
                                    <Icon
                                        fitted
                                        name={ isEditing ? 'edit' : 'edit outline' } 
                                        color={ isEditing ? 'yellow' : 'grey' }    
                                    />
                                </Button>
                            </Grid.Column>
                        }
                    </Grid>
                </Segment>
            </List.Content>
        </List.Item>
    )
}