import { observer } from "mobx-react-lite";
import CardSetHeaderComponent from "../../../components/features/flashCards/setView/CardSetHeaderComponent";
import { Link, useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { useStore } from "../../../stores/core/store";
import { FlashCardSet } from "../../../models/flashCards/flashCardSet";
import { Button, Grid, Loader } from "semantic-ui-react";
import CardSetExpandedComponent, { MovementType } from "../../../components/features/flashCards/setView/SetFlipCardComponent";
import FlashCardSetPagination from "../../../components/features/flashCards/setView/FlashCardSetPagination";
import SetWordsList from "../../../components/features/flashCards/setView/SetWordsList";
import { sleep } from "../../../extensions/asyncExtensions";
import "../../../styles/SetWordsList.css"

export default observer (function FlashCardSetList() {
    const swipeAnimationTime = 0.1;
    const {flashCardStore, accountStore: {isCurrentUser}, promptStore: {openPrompt}} = useStore();
    const {isLoading, loadSet, setIsLoading, deleteSet, clearFilter} = flashCardStore;
    const {id} = useParams();
    const [set, setSet] = useState<FlashCardSet | undefined>(undefined);
    const [index, setIndex] = useState<number>(1);
    const [isFlipped, setIsFlipped] = useState<boolean>(false);
    const [movementType, setMovementType] = useState<MovementType>(MovementType.None);
    const navitage = useNavigate();

    function handleLeftClicked() {
        if(index === 1) return;
        setIsFlipped(false);
        setMovementType(MovementType.Left);
        sleep(swipeAnimationTime).then(() => {
            setIndex(index - 1);
            setMovementType(MovementType.None);
        });
    }

    function handleRightClicked() {
        if(index === set!.words.length) return;
        setIsFlipped(false);
        setMovementType(MovementType.Right);
        sleep(swipeAnimationTime).then(() => {
            setIndex(index + 1);
            setMovementType(MovementType.None);
        });
    }

    function handleFlip() {
        setIsFlipped(!isFlipped);
    }

    function handleMoveEnd() {
        setMovementType(MovementType.None);
    }

    function handleDelete() {
        async function handleDeleteInternal() {
            await deleteSet(id!);
            navitage(-1);
        }
        let content =  (<strong>{`Are you sure you want to delete ${set?.name}?`}</strong>);
        openPrompt({
            open: true, 
            content: {content},
            confirmButton: {
                label: 'Yes, I am sure',
                negative: true,
                icon: 'trash alternate'
            },
            onConfirm: handleDeleteInternal
        });
    }

    useEffect(() => {
        if(id && set === undefined) {
            clearFilter();
            loadSet(id!).then(set => setSet(set));
        }
        return () => {
            setIsLoading(false);
        }
    }, [id, set, loadSet, setIsLoading, setSet, clearFilter])

    return (
        <>
            {isLoading || set === undefined ? (
                <Loader active inline />
            ) : (
                <>
                    <Grid columns='equal' verticalAlign='middle' className='set-words-list-item'>
                        <Grid.Column>
                            <CardSetHeaderComponent setName={set.name} />
                        </Grid.Column>
                        { isCurrentUser(set.meta.createdBy) && 
                            <Grid.Column textAlign='right' >
                                <Button.Group>
                                    <Button
                                        as={Link} 
                                        to={`/edit-set/${id}`}
                                        icon='edit'
                                    />
                                    <Button
                                        icon='trash alternate'
                                        negative
                                        onClick={handleDelete}
                                    />
                                </Button.Group>
                            </Grid.Column>
                        }
                    </Grid>
                    <CardSetExpandedComponent 
                        word={set.words[index - 1]}
                        isFlipped={isFlipped}
                        movementType={movementType}
                        handleClick={handleFlip}
                        handleMoveEnd={handleMoveEnd}
                    />
                    <FlashCardSetPagination 
                        index={index} 
                        wordsCount={set.words.length}
                        handleLeftClicked={handleLeftClicked}
                        handleRightClicked={handleRightClicked} 
                    />
                    <SetWordsList 
                        setId={set.id}
                        isSetOwner={isCurrentUser(set.meta.createdBy)}
                        words={set.words}
                    />
                </>
            )}
        </>
    )
})