using System;

public class CirculartQueue<T>
{
	private enum DEFAULT
	{
		DEFAULT_SIZE = 10
	};

	private T[] m_queue;
	private int m_rear;
	private int m_front;
	private int m_queueSize;

	
	public CirculartQueue()
	{
		m_queue = new T[(int)DEFAULT.DEFAULT_SIZE];
		m_rear = 0;
		m_front = 0;
		m_queueSize = (int)DEFAULT.DEFAULT_SIZE;	//배열사이즈
	}
	
	public CirculartQueue(int Size)
	{
		m_queue = new T[Size];
		m_rear = 0;
		m_front = 0;
		m_queueSize = Size;							//배열사이즈
	}

	public CirculartQueue(CirculartQueue<T> copy)
	{
		m_queue = new T[copy.m_queueSize];
		Array.Copy(copy.m_queue, m_queue, copy.m_queueSize);
		m_rear = copy.m_rear;
		m_front = copy.m_front;
		m_queueSize = copy.m_queueSize;
	}

	public void Enqueue(T Data)
	{
		//배열이 다 찼는가?
		if ((m_rear + 1) % m_queueSize == m_front)
		{
			// 프론트가 앞으로 밀려남
			m_front = (m_front + 1) % m_queueSize;
		}
		m_queue[m_rear] = Data;
		m_rear = (m_rear + 1) % m_queueSize;
	}

	public void Enqueue(in T Data)
	{
		//배열이 다 찼는가?
		if ((m_rear + 1) % m_queueSize == m_front)
		{
			// 프론트가 앞으로 밀려남
			m_front = (m_front + 1) % m_queueSize;
		}
		m_queue[m_rear] = Data;
		m_rear = (m_rear + 1) % m_queueSize;
	}

	public bool Dequeue(T Out)
	{
		//비어있는가?
		if (m_rear == m_front)
		{
			return false;
		}
		Out = m_queue[m_front];
		m_front = (m_front + 1) % m_queueSize;
		return true;
	}

	public bool Dequeue(ref T Out)
	{
		//비어있는가?
		if (m_rear == m_front)
		{
			return false;
		}
		Out = m_queue[m_front];
		m_front = (m_front + 1) % m_queueSize;
		return true;
	}

	public bool Erase(T Data)
	{
		//비어있는가?
		if (m_rear == m_front)
		{
			return false;
		}

		T[] newQueue = new T[m_queueSize];
		T preData = Data;

		int iCnt = 0;
		while (Dequeue(ref preData))
		{
			if (!preData.Equals(Data))
			{
				newQueue[iCnt] = preData;
				++iCnt;
			}
		}

		m_queue = newQueue;
		m_front = 0;
		m_rear = iCnt;

		return true;
	}

	public bool Peek(T Out)
	{
		//비어있는가?
		if (m_rear == m_front)
		{
			return false;
		}
		Out = m_queue[m_front];
		return true;
	}

	public bool Peek(ref T Out)
	{
		//비어있는가?
		if (m_rear == m_front)
		{
			return false;
		}
		Out = m_queue[m_front];
		return true;
	}

	public int GetUseSize()
	{
		if (m_rear > m_front)
		{
			return m_rear - m_front;
		}
		else if (m_rear < m_front)
		{
			return (m_queueSize - m_front) + m_rear;
		}
		else
		{
			return 0;
		}
	}

	public int GetQueueSize()
	{
		return m_queueSize;
	}

	public bool isEmpty()
	{
		if (m_rear == m_front)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public void Release()
	{
		m_rear = 0;
		m_front = 0;
	}


	public T[] GetQueue()
	{
		return m_queue;
	}

	public int GetRear()
	{
		return m_rear;
	}

	public int GetFront()
	{
		return m_front;
	}

	public int GetEndIndex()
	{
		if (m_rear - 1 < 0)
			return m_queueSize - 1;
		else
			return m_rear - 1;
	}

}
