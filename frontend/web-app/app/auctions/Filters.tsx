import { useParamsStore } from '@/hooks/useParamsStore';
import { ButtonGroup } from 'flowbite-react';
import React from 'react'


const pageSizeButtons = [4, 8, 12];

export default function Filters() {
    const pageSize = useParamsStore(state => state.pageSize);
    const setParams = useParamsStore(state => state.setParams);
    
  return (
    <div className='flex justify-between items-center mb-4'>
        <div>
            <span className='uppercase text-sm text-gray-500 mr-2'>Page Size</span>
            <ButtonGroup outline>
                {pageSizeButtons.map((value,index) => (
                    <button
                        key={index}
                        onClick={() => setParams({pageSize:value})}
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
