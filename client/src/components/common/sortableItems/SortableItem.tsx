import { CSS, Transform } from '@dnd-kit/utilities';
import styled from "styled-components";
import { SyntheticListenerMap } from "@dnd-kit/core/dist/hooks/utilities";
import { useSortable } from "@dnd-kit/sortable";
import "../../../styles/Common.css";

interface Props {
    children?: React.ReactNode
    id?: string | number
    isHovering: boolean
    render?: (props: RenderProps) => React.ReactNode
}

interface DraggableDivProps {
    $transform: Transform | null;
    $isDragging: boolean;
    $isHovering: boolean;
}

export interface RenderProps {
    listeners: SyntheticListenerMap | undefined;
}

const DraggableDiv = styled.div.attrs<DraggableDivProps>(({$transform, $isDragging, $isHovering}) => ({
    style: {
        paddingTop: '15px',
        transform: CSS.Translate.toString($transform),
        cursor: $isHovering ? 'move' : 'auto',
        opacity: $isDragging ? 0.4 : 1
    }
}))``

export function SortableItem({children, id, isHovering, render}: Props) {
    const {attributes, listeners, transform, isDragging, setNodeRef} = useSortable({
        id: `${id}` ?? 'draggable'
    });
      
    return (
        <DraggableDiv 
            $transform={transform}
            $isDragging={isDragging}
            $isHovering={isHovering}
            {...attributes}
            ref={setNodeRef}
        >
            {render?.({listeners})}
            {children}
        </DraggableDiv>
    );
}