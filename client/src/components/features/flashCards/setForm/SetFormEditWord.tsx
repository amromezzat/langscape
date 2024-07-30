import { Button, Grid, Segment } from "semantic-ui-react";
import ValidatableTextInput from "../../../common/form/ValidatableTextInput";
import NonInteractableButton from "../../../common/buttons/NonInteractableButton";
import { SyntheticListenerMap } from "@dnd-kit/core/dist/hooks/utilities";
import { useState } from "react";

interface Props {
    wordInputName: string
    translationInputName: string
    position: number
    isDeleted: boolean
    listenerProps?: SyntheticListenerMap | undefined
    ref?: (element: HTMLElement | null) => void;
    onRemove: () => void
    onUpdate: () => void
    onHoverDraggable?: (isHovering: boolean) => void
}

export default function SetFormEditWord(
    {
        wordInputName, 
        translationInputName, 
        position, 
        isDeleted, 
        listenerProps,
        onRemove,
        onUpdate,
        onHoverDraggable
    }: Props) {
    const [isDragActive, setIsDragActive] = useState(true);
        
    return ( 
        <Segment>
            <Grid verticalAlign='middle' divided='vertically'>
                <Grid.Row 
                    className='no-padding-bot' 
                    onMouseEnter={() => onHoverDraggable?.(true)}
                    onMouseLeave={() => onHoverDraggable?.(false)}
                    {...(isDragActive ? listenerProps : {})}
                >
                    <Grid.Column floated='left' textAlign='left' >
                        <h3>{position}</h3>
                    </Grid.Column>
                    <Grid.Column>
                        <Button.Group floated='right'>
                            <NonInteractableButton
                                icon='bars'
                                compact
                            />
                            <Button
                                {...isDeleted ? {
                                    icon: 'redo',
                                    positive: true
                                } : {
                                    icon: 'trash alternate',
                                    negative: true
                                }}
                                compact
                                onClick={onRemove}
                                onMouseEnter={() => setIsDragActive(false)}
                                onMouseLeave={() => setIsDragActive(true)}
                                type='button'
                            />
                        </Button.Group>
                    </Grid.Column>
                </Grid.Row>
                <Grid.Row columns='equal' className='no-padding-top no-padding-bot' >
                    <Grid.Column>
                        <ValidatableTextInput 
                            name={wordInputName}
                            label={{ tag: true, content: 'word' }} 
                            labelPosition='right'
                            disabled={isDeleted}
                            shouldReset={isDeleted}
                            onUpdate={onUpdate}
                        />
                    </Grid.Column>
                    <Grid.Column>
                        <ValidatableTextInput  
                            name={translationInputName}
                            label={{ tag: true, content: 'translation', color: 'teal' }} 
                            labelPosition='right'
                            disabled={isDeleted}
                            shouldReset={isDeleted}
                            onUpdate={onUpdate}
                        />
                    </Grid.Column>
                </Grid.Row>
            </Grid>
        </Segment>
    )
}