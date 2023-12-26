import { DndContext, DragEndEvent, DragMoveEvent, DragStartEvent, UniqueIdentifier } from "@dnd-kit/core";
import { SortableContext, verticalListSortingStrategy } from "@dnd-kit/sortable";
import { Segment } from "semantic-ui-react";
import { DraggableItem } from "./DraggableItem";
import "../../../styles/Common.css"

interface Props {
    children: React.ReactNode
    items: (UniqueIdentifier | {
        id: UniqueIdentifier;
    })[]
    activeItem?: React.ReactNode
    onDragStart?: (event: DragStartEvent) => void
    onDragMove?: (event: DragMoveEvent) => void
    onDragEnd?: (event: DragEndEvent) => void
}

export default function SortableContextContainer({children, items, activeItem, onDragStart, onDragMove, onDragEnd}: Props) {

    return (
        <DndContext onDragStart={onDragStart} onDragMove={onDragMove} onDragEnd={onDragEnd}>
            <SortableContext items={items} strategy={verticalListSortingStrategy}>
                <Segment basic className='no-padding'>
                    {children}
                </Segment>
            </SortableContext>
            <DraggableItem activeItem={activeItem} />
        </DndContext>
    )
}