import { observer } from "mobx-react-lite";
import CardSetHeaderComponent from "../../../components/features/flashCards/setView/CardSetHeaderComponent";
import { Link, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { useStore } from "../../../stores/core/store";
import { FlashCardSet } from "../../../models/flashCards/flashCardSet";
import { Button, Grid, Loader } from "semantic-ui-react";
import CardSetExpandedComponent, { MovementType } from "../../../components/features/flashCards/setView/SetFlipCardComponent";
import FlashCardSetPagination from "../../../components/features/flashCards/setView/FlashCardSetPagination";
import { sleep } from "../../../services/api/core/utility";
import SetWordsList from "../../../components/features/flashCards/setView/SetWordsList";
import "../../../styles/SetWordsList.css"

export default observer (function FlashCardSetList() {
    const swipeAnimationTime = 0.1;
    const {flashCardStore, accountStore: {isCurrentUser}} = useStore();
    const {loadSet, isLoading, setIsLoading} = flashCardStore;
    const {id} = useParams();
    const [set, setSet] = useState<FlashCardSet | undefined>(undefined);
    const [index, setIndex] = useState<number>(1);
    const [isFlipped, setIsFlipped] = useState<boolean>(false);
    const [movementType, setMovementType] = useState<MovementType>(MovementType.None);

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

    useEffect(() => {
        if(id && set === undefined) {
            loadSet(id!).then(set => setSet(set));
        }
        return () => {
            setIsLoading(false);
        }
    }, [id, loadSet, setIsLoading, setSet, set])

    return (
        <>
            {isLoading || set === undefined ? (
                <Loader active inline/>
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