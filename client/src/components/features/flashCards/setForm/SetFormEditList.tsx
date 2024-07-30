import { FieldArray, useField } from "formik";
import { Segment, Grid, Button, Container } from "semantic-ui-react";
import { FlashCardFormWord } from "../../../../models/flashCards/FlashCardFormWord";
import SetFormEditWord from "./SetFormEditWord";
import ErrorPopup from "../../../common/form/ErrorPopup";
import { DragEndEvent, DragStartEvent } from "@dnd-kit/core";
import { useEffect, useState } from "react";
import SortableContextContainer from "../../../common/sortableItems/SortableContextContainer";
import { SortableItem } from "../../../common/sortableItems/SortableItem";
import SetFormEditWordPreview from "./SetFormEditWordPreview";
import '../../../../styles/Common.css'

interface Props {
    words: FlashCardFormWord[],
    isSubmitting: boolean,
    setFieldValue: (field: string, value: any) => void
}

export default function SetFormEditList({ words, isSubmitting, setFieldValue }: Props) {
    const [wordsInputProps, wordsErrorMeta] = useField('words');
    const [activeIndex, setActiveIndex] = useState<number | undefined>(undefined);
    const [hoveredIndex, setHoveredIndex] = useState<number | undefined>(undefined);
    const [activeNode, setActiveNode] = useState<React.ReactNode>();

    useEffect(() => {
        if(activeIndex === undefined) {
            setActiveNode(undefined);
            return;
        }
        
        let node = <SetFormEditWordPreview 
            position={getPosition(activeIndex)} 
            wordInputName={getWordInputName(activeIndex)} 
            translationInputName={getTranslationInputName(activeIndex)} 
        />
        setActiveNode(node);
    }, [activeIndex])

    const getPosition = (index: number) => {
        return index + 1;
    }
    const getWordInputName = (id: number | string) => {
        return `words.${id}.word`;
    }
    const getTranslationInputName = (id: number | string) => {
       return `words.${id}.translation`;
    }
    const getDeletedInputName = (id: number | string) => {
        return `words.${id}.isDeleted`;
    }

    function handleWordRemove(
        word: FlashCardFormWord, 
        index: number, 
        arrayRemove: (index: number) => void) {
        if(!!word.id) {
            setFieldValue(getDeletedInputName(index), !word.isDeleted);
            return;
        }

        arrayRemove(index);
    }

    function handleWordUpdated(word: FlashCardFormWord) {
        word.updated = true;
    }

    function handleHoverDraggable(index: number, isHovering: boolean) {
        if(isHovering) {
            setHoveredIndex(index);
        } else if (index === hoveredIndex) {
            setHoveredIndex(undefined);
        }
    }

    function handleDragStart(event: DragStartEvent) {
        let index = wordsInputProps.value.findIndex((word: FlashCardFormWord) => word.position.toString() === event.active.id.valueOf());
        setActiveIndex(index);
    }

    function handleDragEnd(event: DragEndEvent, fieldArrayMove: (from: number, to: number) => void) {
        const { active, over } = event;
        if (!over || active.id === over.id) {
            setActiveIndex(undefined);
            return;
        }
    
        const activeIndex = active?.data?.current?.sortable?.index;
        const overIndex = over?.data?.current?.sortable?.index;
        if (activeIndex >= 0 && overIndex >= 0) {
            fieldArrayMove(activeIndex, overIndex);
        }

        setActiveIndex(undefined);
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
                render={({push, remove, move}) => (
                    <>
                        <SortableContextContainer
                            items={words.map(word => `${word.position}`)}
                            activeItem={activeNode}
                            onDragStart={handleDragStart}
                            onDragEnd={event => handleDragEnd(event, move)}
                        >
                            {words.map((word, index) => {
                                return <SortableItem
                                    key={word.position}  
                                    id={word.position}
                                    isHovering={hoveredIndex === index || activeIndex === index}
                                    render={({listeners}) => (
                                        <SetFormEditWord
                                            wordInputName={getWordInputName(index)}
                                            translationInputName={getTranslationInputName(index)}
                                            position={getPosition(index)}
                                            isDeleted={word.isDeleted}
                                            listenerProps={listeners}
                                            onRemove={() => handleWordRemove(word, index, remove)}
                                            onUpdate={() => handleWordUpdated(word)}
                                            onHoverDraggable={(isHovering) => handleHoverDraggable(index, isHovering)}
                                        />
                                    )}
                                />
                            })}
                        </SortableContextContainer>

                        <Segment clearing>
                            <Grid verticalAlign='middle' divided='vertically'>
                                <Grid.Row className='no-padding-bot'>
                                    <Grid.Column floated='left' textAlign='left'>
                                        <h3>{getPosition(words.length)}</h3>
                                    </Grid.Column>
                                </Grid.Row>
                                <Grid.Row className='no-padding-top no-padding-bot'>
                                    <Grid.Column>
                                        <Button
                                            onClick={() => push(new FlashCardFormWord("", "", words.length))}
                                            primary
                                            disabled={isSubmitting}
                                            type='button'
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
            />
        </>
    )
}