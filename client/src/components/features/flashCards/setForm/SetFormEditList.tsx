import { FieldArray, useField } from "formik";
import { Segment, Grid, Button, Container } from "semantic-ui-react";
import { FlashCardFormWord } from "../../../../models/flashCards/FlashCardFormWord";
import SetFormEditWord from "./SetFormEditWord";
import ErrorPopup from "../../../common/form/ErrorPopup";
import '../../../../styles/Common.css'

interface Props {
    words: FlashCardFormWord[],
    isSubmitting: boolean,
    setFieldValue: (field: string, value: any) => void
}

export default function SetFormEditList({ words, isSubmitting, setFieldValue }: Props) {
    const [, wordsErrorMeta] = useField('words');

    const getWordInputName = (index: number) => {
        return `words.${index}.word`;
    }
    const getTranslationInputName = (index: number) => {
       return `words.${index}.translation`;
    }
    const getDeletedInputName = (index: number) => {
        return `words.${index}.isDeleted`;
    }

    function removeWord(
        word: FlashCardFormWord, 
        index: number, 
        arrayRemove: (index: number) => void) {
        if(!!word.id) {
            setFieldValue(getDeletedInputName(index), !word.isDeleted);
            return;
        }

        arrayRemove(index);
    }

    return (
        <>
            <ErrorPopup
                trigger={<Container textAlign='right' className='expanded'></Container>} 
                touched={wordsErrorMeta.value.length === 0 && typeof wordsErrorMeta.error === 'string'} 
                error={wordsErrorMeta.error}
            />
            <FieldArray 
                name={'words'}
                render={({push, remove}) => {
                    return (
                        <>
                            { words.map((word, index) => {
                                return <SetFormEditWord 
                                    wordInputName={getWordInputName(index)} 
                                    translationInputName={getTranslationInputName(index)} 
                                    index={index + 1}
                                    isDeleted={word.isDeleted}
                                    handleRemove={() => removeWord(word, index, remove)}
                                    key={index}
                                />
                            }) }
                            <Segment clearing>
                                <Grid verticalAlign='middle' divided='vertically'>
                                    <Grid.Row className='no-padding-bot'>
                                        <Grid.Column floated='left' textAlign='left' >
                                            <h3>{words.length + 1}</h3>
                                        </Grid.Column>
                                    </Grid.Row>
                                    <Grid.Row className='no-padding-top no-padding-bot'>
                                        <Grid.Column>
                                            <Button
                                                onClick={() => push(new FlashCardFormWord("", "", words.length))}
                                                primary
                                                disabled={isSubmitting}
                                            >
                                                Add Word
                                            </Button>
                                        </Grid.Column>
                                    </Grid.Row>
                                </Grid>
                            </Segment>
                        </>
                    )
                }
            }
            />
        </>
    )
}