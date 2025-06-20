import { ButtonGroup } from 'flowbite-react';
import React from 'react'

type Props = {
    pageSize: number;
    setPageSize: (pageSize: number) => void;
}

const pageSizeButtons = [4, 8, 12];

export default function Filters({pageSize,setPageSize}: Props) {
  return (
    <div className='flex justify-between items-center mb-4'>
        <div>
            <span className='uppercase text-sm text-gray-500 mr-2'>Page Size</span>
            <ButtonGroup outline>
                {pageSizeButtons.map((value,index) => (
                    <button
                        key={index}
                        onClick={() => setPageSize(value)}
                        color={`${pageSize === value ? 'red' : 'gray'}`}
                        className='focus:ring-0'
                    >
                       {value}
                    </button>
                ))}
            </ButtonGroup>
        </div>
    </div>
  )
}
